using System.Diagnostics;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace TrackConversion
{
    public class TracPlus_RockAIR
    {
        #region Private Variables

        //private string[] Colours = { "Magenta", "Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Violet", "Black", "White" };
        private double _Altitude;
        private double _Bearing;
        private double _Distance;
        private string _Dop = "";
        private TimeSpan _Elapsed;
        private string _Event = "";
        private string _Gateway = "";
        private static double _LastValidAltitude = 0;
        private static double _LastValidBearing = 0;
        private static double _LastValidDistance = 0;
        private static TimeSpan _LastValidElapsed = new TimeSpan(0, 0, 0);
        private static TimeSpan _LastValidLatency = new TimeSpan(0, 0, 0);
        private static double _LastValidLatitude = 0;
        private static double _LastValidLongitude = 0;
        private static double _LastValidSpeed = 0;
        private TimeSpan _Latency;
        private double _Latitude;
        private double _Longitude;
        private string _Metadata = "";
        private double _Speed;
        private DateTime _Timestamp;
        private static string _FileHeader = @"<?xml version=""1.0""?><gpx version=""1.1"" creator=""GeoResults"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:ogr=""http://osgeo.org/gdal"" xmlns=""http://www.topografix.com/GPX/1/1"" xsi:schemaLocation=""http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd""><metadata><link href=""http://www.georesults.com.au""><text>GeoResults</text></link></metadata>";
        private static string _WaypointDetail = @"<wpt lat=""##LAT##"" lon=""##LON##""><name>##NAME##</name></wpt>";
        private static string _TrackHeader = @"<trk><name>##NAME##</name><extensions><gpxx:TrackExtension xmlns:gpxx=""http://www.garmin.com/xmlschemas/GpxExtensions/v3""><gpxx:DisplayColor>##COLOUR##</gpxx:DisplayColor></gpxx:TrackExtension></extensions><trkseg>";
        private static string _TrackPointDetail = @"<trkpt lat=""##LAT##"" lon=""##LON##""><ele>##ALT##</ele><time>##TIME##</time></trkpt>";
        private static string _TrackFooter = @"</trkseg></trk>";
        private static string _FileFooter = @"</gpx>";

        #endregion

        #region Properties

        public double Altitude
        {
            get
            {
                return _Altitude;
            }
            set
            {
                _Altitude = value;
            }
        }

        public double Bearing
        {
            get
            {
                return _Bearing;
            }
            set
            {
                _Bearing = value;
            }
        }

        public double Distance
        {
            get
            {
                return _Distance;
            }
            set
            {
                _Distance = value;
            }
        }

        public string Dop
        {
            get
            {
                return _Dop;
            }
            set
            {
                _Dop = value;
            }
        }

        public TimeSpan Elapsed
        {
            get
            {
                return _Elapsed;
            }
            set
            {
                _Elapsed = value;
            }
        }

        public string Event
        {
            get
            {
                return _Event;
            }
            set
            {
                _Event = value;
            }
        }

        public string Gateway
        {
            get
            {
                return _Gateway;
            }
            set
            {
                _Gateway = value;
            }
        }

        public TimeSpan Latency
        {
            get
            {
                return _Latency;
            }
            set
            {
                _Latency = value;
            }
        }

        public double Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                _Latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                _Longitude = value;
            }
        }

        public string Metadata
        {
            get
            {
                return _Metadata;
            }
            set
            {
                _Metadata = value;
            }
        }

        public double Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                _Timestamp = value;
            }
        }

        #endregion

        /// <summary>
        /// Ensure the data is consistent.  Set max speed, max rate of climb etc
        /// </summary>
        /// <param name="records"></param>
        /// <returns></returns>
        private static List<TracPlus_RockAIR> Cleanup(List<TracPlus_RockAIR> Records, bool ZeroInvalidData)
        {
            List<TracPlus_RockAIR> Results = Records;
            double PreviousLatitude = 0;
            double PreviousLongitude = 0;
            double PreviousAltitude = 0;
            DateTime PreviousTimestamp = DateTime.MinValue;
            int Counter = 0;

            foreach (TracPlus_RockAIR Record in Records)
            {
                if (PreviousLatitude != 0 && PreviousLongitude != 0)
                {
                    // Calculate distance between the two points
                    double DistanceInMetres = GetDistanceBetweenTwoPoints(PreviousLatitude, PreviousLongitude, Record.Latitude, Record.Longitude) * 1000;

                    // Now determine the speed
                    if (PreviousTimestamp != DateTime.MinValue)
                    {
                        TimeSpan Period = Record.Timestamp - PreviousTimestamp;

                        double MetresPerSecond = Math.Round(DistanceInMetres / Period.TotalSeconds, 2);
                        double CalculatedSpeed = Math.Round(MetresPerSecond * 18 / 5, 1);

                        double SpeedDifferencePercent = Math.Round(Math.Abs((CalculatedSpeed - Record.Speed) / CalculatedSpeed * 100), 1);

                        if (SpeedDifferencePercent > 5)
                        {
                            //Debug.Print($"++ Applied calculated speed ({CalculatedSpeed} vs {Record.Speed}) km/h for location {Record.Latitude} {Record.Longitude}");

                            if (CalculatedSpeed > 170)
                            {
                                Debug.Print("WTF!!");
                            }
                            else
                            {
                                Record.Speed = CalculatedSpeed;
                            }
                        }
                        else
                        {
                            //Debug.Print($"Using reported speed for location {Record.Latitude} {Record.Longitude}");
                        }
                    }

                    // Calculate the gradient
                    double AltitudeDifference = Math.Abs(Record.Altitude - PreviousAltitude);
                    double Gradient = AltitudeDifference / DistanceInMetres * 100;

                    if (Gradient > 30)
                    {
                        if (ZeroInvalidData)
                        {
                            Record.Altitude = 0;
                        }
                        else
                        {
                            if (DistanceInMetres > 45)
                            {
                                try
                                {
                                    Record.Altitude = GetAltitude(Record.Latitude, Record.Longitude);

                                    Debug.Print($"** Gradient too high ({Gradient}): determined Altitude ({Record.Altitude}) for location {Record.Latitude} {Record.Longitude} from Web Service.");

                                    Debug.Print($"Cleanup progress:  {Counter} of {Records.Count}");
                                }
                                catch
                                {
                                    Record.Altitude = PreviousAltitude;

                                    //Debug.Print($"++ Used previous Altitude ({Record.Altitude}) for location {Record.Latitude} {Record.Longitude}.");
                                }
                            }
                            else
                            {
                                // Use the previous altitude
                                Record.Altitude = PreviousAltitude;

                                //Debug.Print($"++ Used previous Altitude ({Record.Altitude}) for location {Record.Latitude} {Record.Longitude}.");
                            }
                        }
                    }
                    else
                    {
                        //Debug.Print($"Using reported Altitude ({Record.Altitude}) for location {Record.Latitude} {Record.Longitude}.");
                    }
                }

                if (Record.Altitude < 1)
                {
                    if (ZeroInvalidData)
                    {
                        Record.Altitude = 0;
                    }
                    else
                    {
                        try
                        {
                            double DistanceInMetres = GetDistanceBetweenTwoPoints(PreviousLatitude, PreviousLongitude, Record.Latitude, Record.Longitude) * 1000;

                            if (DistanceInMetres < 45)
                            {
                                Record.Altitude = PreviousAltitude;
                            }
                            else
                            {
                                Record.Altitude = GetAltitude(Record.Latitude, Record.Longitude);

                                Debug.Print($"** Altitude <0: determined Altitude ({Record.Altitude}) for location {Record.Latitude} {Record.Longitude} from Web Service.");
                            }

                            Debug.Print($"Cleanup progress:  {Counter} of {Records.Count}");
                        }
                        catch
                        {
                            Record.Altitude = PreviousAltitude;

                            //Debug.Print($"++ Used previous Altitude ({Record.Altitude}) for location {Record.Latitude} {Record.Longitude}.");
                        }
                    }
                }

                PreviousAltitude = Record.Altitude;
                PreviousLatitude = Record.Latitude;
                PreviousLongitude = Record.Longitude;
                PreviousTimestamp = Record.Timestamp;

                Counter++;

                //Debug.Print("======================================================");
                //Debug.Print($"Cleanup progress:  {Counter} of {Records.Count}");
            }

            return Results;
        }

        private static DateTime ConvertTimestamp(string Input)
        {

            // 31 Oct 2022 11:45:05 AWST

            // Get first colon
            int Colon1Pos = Input.IndexOf(":");
            // Now find the first space to the left of this
            string Temp = Input.Substring(0, Colon1Pos);
            int Space1Pos = Temp.LastIndexOf(" ");

            // Get last colon
            int Colon2Pos = Input.LastIndexOf(":");
            // Now get the first space to the right of this
            int Space2Pos = Input.IndexOf(" ", Colon2Pos);

            if (Space2Pos == -1)
            {
                Space2Pos = Input.Length;
            }

            DateOnly Date = new DateOnly(1970, 1, 1);
            TimeOnly Time = TimeOnly.MinValue;

            try
            {
                Date = DateOnly.Parse(Input.Substring(0, Space1Pos));
                Time = TimeOnly.Parse(Input.Substring(Space1Pos, Space2Pos - Space1Pos));
            }
            catch
            {
                Debug.Print("Error with date formatting");
            }

            return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hour, Time.Minute, Time.Second);
        }

        private static TracPlus_RockAIR FromCsv(string csvLine, bool ZeroEmptyValues)
        {
            string[] values = csvLine.Split(',');

            int[] unused = { };


            int[] Indexes;
            // How many columns?
            // What formats are:
            //   Altitude (ft/m)
            //   Speed (kn/mi/km)
            //   Latitude and Longitude (WGS84/Decimal)
            //   Time (many formats to consider)

            if (values.Length == 20)
            {
                Indexes = new int[] { 3, 4, 2, 5, 6, 7, 10, 14, 15, 16, 17, 18, 1 };
            }
            else
            {
                Indexes = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }; // Lat, Long, Time, Alt, Bearing, Speed, Event, Dop, Latency, Elapsed, Distance, Gateway, Metadata
            }

            TracPlus_RockAIR GPSData = new TracPlus_RockAIR();
            //{
            try
            {
                if (values[Indexes[0]].Length > 0)
                {
                    GPSData.Latitude = Convert.ToDouble(values[Indexes[0]].ToString());

                    _LastValidLatitude = GPSData.Latitude;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Latitude = 0;
                    }
                    else
                    {
                        GPSData.Latitude = _LastValidLatitude;
                    }
                    //Debug.WriteLine("No Latitude information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Latitude could not be converted: " + ex.ToString());
            }

            try
            {
                if (values[Indexes[1]].Length > 0)
                {
                    GPSData.Longitude = Convert.ToDouble(values[Indexes[1]]);

                    _LastValidLongitude = GPSData.Longitude;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Longitude = 0;
                    }
                    else
                    {
                        GPSData.Longitude = _LastValidLongitude;
                    }
                    //Debug.WriteLine("No Longitude information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Longitude could not be converted: " + ex.ToString());
            }

            GPSData.Timestamp = ConvertTimestamp(values[Indexes[2]]);

            try
            {
                if (values[Indexes[3]].Length > 0)
                {
                    GPSData.Altitude = Convert.ToDouble(values[Indexes[3]]);

                    _LastValidAltitude = GPSData.Altitude;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Altitude = 0;
                    }
                    else
                    {
                        GPSData.Altitude = _LastValidAltitude;
                    }
                    //Debug.WriteLine("No Altitude information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Altitude could not be converted: " + ex.ToString());
            }

            try
            {
                if (values[Indexes[4]].Length > 0)
                {
                    GPSData.Bearing = Convert.ToDouble(values[Indexes[4]]);

                    _LastValidBearing = GPSData.Bearing;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Bearing = 0;
                    }
                    else
                    {
                        GPSData.Bearing = _LastValidBearing;
                    }
                    //Debug.WriteLine("No Bearing information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Bearing could not be converted: " + ex.ToString());
            }

            try
            {
                if (values[Indexes[5]].Length > 0)
                {
                    GPSData.Speed = Convert.ToDouble(values[Indexes[5]]);

                    _LastValidSpeed = GPSData.Speed;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Speed = 0;
                    }
                    else
                    {
                        GPSData.Speed = _LastValidSpeed;
                    }
                    //Debug.WriteLine("No Speed information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Speed could not be converted: " + ex.ToString());
            }

            GPSData.Event = Convert.ToString(values[Indexes[6]]);
            GPSData.Dop = Convert.ToString(values[Indexes[7]]);

            try
            {
                if (values[Indexes[8]].Length > 0)
                {
                    GPSData.Latency = TimeSpan.Parse(values[Indexes[8]]);

                    if (GPSData.Latency.Days > 0)
                    {
                        GPSData.Latency = new TimeSpan(0, 0, Convert.ToInt32(values[Indexes[8]]));
                    }

                    _LastValidLatency = GPSData.Latency;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Latency = new TimeSpan(0, 0, 0);
                    }
                    else
                    {
                        GPSData.Latency = _LastValidLatency;
                    }
                    //Debug.WriteLine("No Latency information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Latency could not be converted: " + ex.ToString());
            }

            try
            {
                if (values[Indexes[9]].Length > 0)
                {
                    GPSData.Elapsed = TimeSpan.Parse(values[Indexes[9]]);

                    if (GPSData.Elapsed.Days > 0)
                    {
                        GPSData.Elapsed = new TimeSpan(0, 0, Convert.ToInt32(values[Indexes[9]]));
                    }

                    _LastValidElapsed = GPSData.Elapsed;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Elapsed = new TimeSpan(0, 0, 0);
                    }
                    else
                    {
                        GPSData.Elapsed = _LastValidElapsed;
                    }
                    //Debug.WriteLine("No Elapsed information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Elapsed could not be converted: " + ex.ToString());
            }

            try
            {
                if (values[Indexes[10]].Length > 0)
                {
                    GPSData.Distance = Convert.ToDouble(values[Indexes[10]]);

                    _LastValidDistance = GPSData.Distance;
                }
                else
                {
                    if (ZeroEmptyValues)
                    {
                        GPSData.Distance = 0;
                    }
                    else
                    {
                        GPSData.Distance = _LastValidDistance;
                    }
                    //Debug.WriteLine("No Distance information in " + csvLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Distance could not be converted: " + ex.ToString());
            }
            GPSData.Gateway = Convert.ToString(values[Indexes[11]]);
            GPSData.Metadata = Convert.ToString(values[Indexes[12]]);
            //};

            return GPSData;
        }

        private static double GetAltitude(double Latitude, double Longitude)
        {
            double Elevation = 0;
            HttpClient httpClient = new HttpClient();
            string URL = $"https://www.freemaptools.com/ajax/elevation-service.php?v=4&lat={Latitude}&lng={Longitude}";

            httpClient.DefaultRequestHeaders.Add("referer", "https://www.freemaptools.com/elevation-finder.htm");

            try
            {
                string Result = httpClient.GetStringAsync(URL).Result;

                JsonNode? obj = JsonObject.Parse(Result).ToJsonString();

                Elevation = Convert.ToDouble(JsonObject.Parse(Result).ToJsonString().Split(",")[2].Split(":")[1].Split("}")[0]);
            }
            catch
            {

            }
            return Elevation;
        }

        /// <summary>
        /// Calculate distance between two points.  Uses Haversine formula.  https://en.wikipedia.org/wiki/Haversine_formula
        /// </summary>
        /// <param name="Latitude1"></param>
        /// <param name="Longitude1"></param>
        /// <param name="Latitude2"></param>
        /// <param name="Longitude2"></param>
        /// <returns></returns>
        public static double GetDistanceBetweenTwoPoints(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {
            const double p = 0.017453292519943295;    // Math.PI / 180
            Func<double, double> c = Math.Cos;

            double a = 0.5 - (c((Latitude2 - Latitude1) * p) / 2) +
                             (c(Latitude1 * p) * c(Latitude2 * p) *
                             (1 - c((Longitude2 - Longitude1) * p)) / 2);

            return 12742 * Math.Asin(Math.Sqrt(a)); // 2 * R; R = 6371 km
        }

        public static string ToGPX(string Filename, bool Reverse, string Colour, bool ZeroInvalidData)
        {
            string Subject = Path.GetFileName(Path.GetFileNameWithoutExtension(Filename));
            string OutputFilename = Path.Combine(Path.GetDirectoryName(Filename) + "", Path.GetFileNameWithoutExtension(Filename)) + ".gpx";
            List<TracPlus_RockAIR>? OrderedRecords = null;
            List<TracPlus_RockAIR>? TrackPoints = null;
            List<TracPlus_RockAIR>? Waypoints = null;

            IEnumerable<TracPlus_RockAIR>? UnorderedData = File.ReadAllLines(Filename)
                                                               .Skip(1)
                                                               .Select(v => TracPlus_RockAIR.FromCsv(v, ZeroInvalidData));

            if (Reverse)
            {
                OrderedRecords = UnorderedData.OrderBy(v => v.Timestamp).ToList();
            }
            else
            {
                OrderedRecords = UnorderedData.OrderByDescending(v => v.Timestamp).ToList();
            }

            OrderedRecords = TracPlus_RockAIR.Cleanup(OrderedRecords, ZeroInvalidData);

            Waypoints = OrderedRecords.Where(v => v.Event is not "Standard").ToList();

            TrackPoints = OrderedRecords.Where(v => v.Event is "Standard").ToList();

            StringBuilder Output = new StringBuilder();

            StringBuilder unused5 = Output.Append(_FileHeader);

            foreach (TracPlus_RockAIR Waypoint in Waypoints)
            {
                StringBuilder unused4 = Output.Append(_WaypointDetail.Replace("##LAT##", Waypoint.Latitude.ToString())
                                                                     .Replace("##LON##", Waypoint.Longitude.ToString())
                                                                     .Replace("##NAME##", Waypoint.Event));

            }

            StringBuilder unused3 = Output.Append(_TrackHeader.Replace("##NAME##", Subject)
                                                              .Replace("##COLOUR##", Colour));

            foreach (TracPlus_RockAIR TrackPoint in TrackPoints)
            {
                StringBuilder unused2 = Output.Append(_TrackPointDetail.Replace("##LAT##", TrackPoint.Latitude.ToString())
                                                                       .Replace("##LON##", TrackPoint.Longitude.ToString())
                                                                       .Replace("##ALT##", TrackPoint.Altitude.ToString())
                                                                       .Replace("##TIME##", TrackPoint.Timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ")));

            }

            StringBuilder unused1 = Output.Append(_TrackFooter);

            StringBuilder unused = Output.Append(_FileFooter);

            return GetPrettyXml(Output.ToString());
        }

        private static string GetPrettyXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }
    }
}

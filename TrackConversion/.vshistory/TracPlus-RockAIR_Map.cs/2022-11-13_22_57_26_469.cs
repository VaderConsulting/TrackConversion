using CsvHelper.Configuration;

namespace TrackConversion
{
    public sealed class TracPlus_RockAIR_Map : ClassMap<TracPlus_RockAIR>
    {
        public TracPlus_RockAIR_Map()
        {
            var unused12 = Map(m => m.Latitude);
            var unused11 = Map(m => m.Longitude);
            var unused10 = Map(m => m.Timestamp);
            var unused9 = Map(m => m.Altitude);
            var unused8 = Map(m => m.Bearing);
            var unused7 = Map(m => m.Speed);
            var unused6 = Map(m => m.Event);
            var unused5 = Map(m => m.Dop);
            var unused4 = Map(m => m.Latency);
            var unused3 = Map(m => m.Elapsed);
            var unused2 = Map(m => m.Distance);
            var unused1 = Map(m => m.Gateway);
            var unused = Map(m => m.Metadata);
        }
    }
}

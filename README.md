# TrackConversion

A .NET 6 Windows Forms utility that converts **TracPlus** and **RockAIR** GPS tracking CSV export files into standard **GPX 1.1** format, ready for import into Garmin BaseCamp, Google Earth, QGIS, or any GPX-compatible mapping tool.

**Source last updated:** 2022-11-13

**Initiated:** 2022-11-13 · **Framework:** .NET 6 Windows Forms · **Solution:** `TrackConversion.sln`

---

## Overview

TracPlus and RockAIR are satellite/cellular tracking systems used in aviation, maritime, and fleet operations. TrackConversion reads one or more CSV files, parses position records, and writes a corresponding `.gpx` file alongside each input file.

---

## Features

- **Batch conversion** - add any number of CSV files; all converted in one click
- **GPX 1.1 output** with Garmin `gpxx:TrackExtension` colour tags for coloured track display
- **15 track colours** - automatically cycled across input files
- **Reverse track** option - reverses the chronological order of track points
- **Zero invalid data** option - replaces out-of-range sensor values with zero
- **Output adjacent to input** - converted `.gpx` sits next to the `.csv`

---

## Technology Stack

| Component | Detail |
|-----------|--------|
| Runtime | .NET 6 Windows |
| UI Framework | Windows Forms |
| CSV Parsing | CsvHelper 30.0.1 |

---

## Usage

1. Launch **TrackConversion.exe**
2. Click **Add Input File** and select one or more `.csv` files
3. Optionally tick **Reverse** and/or **Zero Invalid Data**
4. Click **Convert**

## Requirements

- Visual Studio 2022, .NET 6.0, .NET 7.0


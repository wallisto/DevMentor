using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WeatherLibrary
{
    public class CsvWeatherService : IWeatherService
    {
        private string root;

        public CsvWeatherService(string rootDirectory)
        {
            this.root = rootDirectory;
        }

        public IEnumerable<string> GetWeatherStations()
        {
            return from csvFile in new DirectoryInfo(root).EnumerateFiles()
                   select csvFile.Name.Substring(0, csvFile.Name.Length - ".csv".Length);
        }

        public IEnumerable<WeatherSample> GetWeatherSamples(string station)
        {
            string csvFile = Path.Combine(root, station + ".csv");

            return from row in File.ReadLines(csvFile).Skip(1)
                   let columns = row.Split(',')
                   let max = ColumnParser(columns[2], v => decimal.Parse(v))
                   let min = ColumnParser(columns[3], v => decimal.Parse(v))
                   let rain = ColumnParser(columns[5], v => decimal.Parse(v))
                   select new WeatherSample(
                       int.Parse(columns[0]),
                       int.Parse(columns[1]),
                      max, min, rain);
        }

        private static Nullable<T> ColumnParser<T>(string val, Func<string, T> parser) where T : struct
        {

            if (val.Trim() == "---") return null;

            return parser(val);
        }
    }

   
}

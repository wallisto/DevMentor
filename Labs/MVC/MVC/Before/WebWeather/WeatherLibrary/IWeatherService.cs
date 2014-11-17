using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherLibrary
{
    public class WeatherSample
    {
        public WeatherSample(int year, int month , decimal? max , decimal? min , decimal? rainFall)
        {
            Year = year;
            Month = month;
            MaxTemperature = max;
            MinTemperature = min;
            RainFall = rainFall;
        }
        public int Year { get; private set; }
        public int Month { get; private set; }

        public decimal? MaxTemperature { get; private set; }
        public decimal? MinTemperature { get; private set; }
        public decimal? RainFall { get; private set; }
     

    }

    public interface IWeatherService
    {
        IEnumerable<string> GetWeatherStations();

        IEnumerable<WeatherSample> GetWeatherSamples(string station);
    }
}

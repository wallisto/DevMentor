using System.Collections.Generic;
using WeatherLibrary;

namespace WebWeather.Models
{
    public class WeatherStationModel
    {
        IWeatherService service;

        public WeatherStationModel(IWeatherService service, string station)
        {
            this.service = service;
            Name = station;
        }
        public string Name { get; private set; }

        public IEnumerable<WeatherSample> Samples
        {
            get { return service.GetWeatherSamples(Name); }
        }
    }

}
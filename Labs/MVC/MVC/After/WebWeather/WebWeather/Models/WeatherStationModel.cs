using System.Collections.Generic;
using System.Linq;
using WeatherLibrary;

namespace WebWeather.Models
{
    public class WeatherStationModel
    {
        IWeatherService service;
        private const int pageSize = 10;

        public WeatherStationModel(IWeatherService service,string station)
        {
            this.service = service;
            Name = station;
        }
        public string Name { get; private set; }
        public int PageNumber { get; set; }

        public IEnumerable<WeatherSample> Samples
        {
            get { return service.GetWeatherSamples(Name); }
        }

        public IEnumerable<WeatherSample> Page
        {
            get
            {
                return Samples.Skip(PageNumber*pageSize).Take(pageSize);
            }
        }
    }

}
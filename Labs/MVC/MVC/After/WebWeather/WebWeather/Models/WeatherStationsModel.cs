using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeatherLibrary;

namespace WebWeather.Models
{
    public class WeatherStationsModel
    {

        private IWeatherService service;

        public WeatherStationsModel(IWeatherService weatherService)
        {
            service = weatherService;
        }

        public IEnumerable<string> Stations
        {
            get { return service.GetWeatherStations(); }
        }

        public IEnumerable<IGrouping<string, string>> StationGroups
        {
            get
            {
                return service.GetWeatherStations()
                    .GroupBy(s => s.First().ToString(), s => s);
            }
        }


    }
}
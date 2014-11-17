using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherLibrary;
using WebWeather.Models;

namespace WebWeather.Controllers
{
    public class WeatherController : Controller
    {
        private IWeatherService weatherService;
        //
        // GET: /Weather/
        public WeatherController()
        {
            weatherService = new CsvWeatherService(System.Web.HttpContext.Current.Server.MapPath("~/WeatherData"));
        }

        
        //
        // GET: /Weather/

        public ActionResult Index()
        {
            ViewData.Model = new WeatherStationsModel(weatherService);
            return View();
        }

    }
}

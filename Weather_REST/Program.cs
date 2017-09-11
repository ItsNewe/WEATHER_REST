using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Weather_REST
{
    class Program
    {
        //------GELOC CLASSES------
        public class Geoname
        {
            public string adminCode1 { get; set; }
            public string lng { get; set; }
            public int geonameId { get; set; }
            public string toponymName { get; set; }
            public string countryId { get; set; }
            public string fcl { get; set; }
            public int population { get; set; }
            public string countryCode { get; set; }
            public string name { get; set; }
            public string fclName { get; set; }
            public string countryName { get; set; }
            public string fcodeName { get; set; }
            public string adminName1 { get; set; }
            public string lat { get; set; }
            public string fcode { get; set; }
        }

        public class RootObject
        {
            public int totalResultsCount { get; set; }
            public List<Geoname> geonames { get; set; }
        }

        //-------------WEATHER CLASSES----------------
        public class Currently
        {
            public int time { get; set; }
            public string summary { get; set; }
            public string icon { get; set; }
            public int nearestStormDistance { get; set; }
            public double precipIntensity { get; set; }
            public double precipProbability { get; set; }
            public double temperature { get; set; }
            public double apparentTemperature { get; set; }
            public double dewPoint { get; set; }
            public double humidity { get; set; }
            public double pressure { get; set; }
            public double windSpeed { get; set; }
            public double windGust { get; set; }
            public int windBearing { get; set; }
            public double cloudCover { get; set; }
            public int uvIndex { get; set; }
            public double visibility { get; set; }
            public double ozone { get; set; }
        }

        public class Datum
        {
            public int time { get; set; }
            public double precipIntensity { get; set; }
            public double precipProbability { get; set; }
        }

        public class Minutely
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum> data { get; set; }
        }

        public class Datum2
        {
            public int time { get; set; }
            public string summary { get; set; }
            public string icon { get; set; }
            public double precipIntensity { get; set; }
            public double precipProbability { get; set; }
            public double temperature { get; set; }
            public double apparentTemperature { get; set; }
            public double dewPoint { get; set; }
            public double humidity { get; set; }
            public double pressure { get; set; }
            public double windSpeed { get; set; }
            public double windGust { get; set; }
            public int windBearing { get; set; }
            public double cloudCover { get; set; }
            public int uvIndex { get; set; }
            public double visibility { get; set; }
            public double ozone { get; set; }
            public string precipType { get; set; }
        }

        public class Hourly
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum2> data { get; set; }
        }

        public class Datum3
        {
            public int time { get; set; }
            public string summary { get; set; }
            public string icon { get; set; }
            public int sunriseTime { get; set; }
            public int sunsetTime { get; set; }
            public double moonPhase { get; set; }
            public double precipIntensity { get; set; }
            public double precipIntensityMax { get; set; }
            public int precipIntensityMaxTime { get; set; }
            public double precipProbability { get; set; }
            public string precipType { get; set; }
            public double temperatureHigh { get; set; }
            public int temperatureHighTime { get; set; }
            public double temperatureLow { get; set; }
            public int temperatureLowTime { get; set; }
            public double apparentTemperatureHigh { get; set; }
            public int apparentTemperatureHighTime { get; set; }
            public double apparentTemperatureLow { get; set; }
            public int apparentTemperatureLowTime { get; set; }
            public double dewPoint { get; set; }
            public double humidity { get; set; }
            public double pressure { get; set; }
            public double windSpeed { get; set; }
            public double windGust { get; set; }
            public int windGustTime { get; set; }
            public int windBearing { get; set; }
            public double cloudCover { get; set; }
            public int uvIndex { get; set; }
            public int uvIndexTime { get; set; }
            public double visibility { get; set; }
            public double ozone { get; set; }
            public double temperatureMin { get; set; }
            public int temperatureMinTime { get; set; }
            public double temperatureMax { get; set; }
            public int temperatureMaxTime { get; set; }
            public double apparentTemperatureMin { get; set; }
            public int apparentTemperatureMinTime { get; set; }
            public double apparentTemperatureMax { get; set; }
            public int apparentTemperatureMaxTime { get; set; }
        }

        public class Daily
        {
            public string summary { get; set; }
            public string icon { get; set; }
            public List<Datum3> data { get; set; }
        }

        public class Flags
        {
            public List<string> sources { get; set; }
            public List<string> isdstations { get; set; }
            public string units { get; set; }
        }

        public class RootObject2
            {
                public double latitude { get; set; }
                public double longitude { get; set; }
                public string timezone { get; set; }
                public Currently currently { get; set; }
                public Minutely minutely { get; set; }
                public Hourly hourly { get; set; }
                public Daily daily { get; set; }
                public Flags flags { get; set; }
                public int offset { get; set; }
            }



    static void Main(string[] args)
        {
            string DarkSkyAPIKey = "token";

            string lng;
            string lat;
            Console.Write("Enter a city: ");
            string query = Console.ReadLine();
            Console.WriteLine("For wich days do you want the forecast? (0 for today)");
            int daychoice = Convert.ToInt32(Console.ReadLine());
            if(daychoice.ToString() == null)
            {
                daychoice = 0;
            }
             using (var httpClient = new HttpClient())
            {

                var response = httpClient.GetStringAsync(new Uri("http://api.geonames.org/searchJSON?q="+query+"&maxRows=1&username=newe")).Result;

                var resjs = JsonConvert.DeserializeObject<RootObject>(response);
                var geodata = resjs.geonames[0];
                lng = geodata.lng;
                lat = geodata.lat;
            }
            

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(new Uri($"https://api.darksky.net/forecast/{DarkSkyAPIKey}/{lat},{lng}?units=si&lang=en")).Result;
                var rescpt = JsonConvert.DeserializeObject<RootObject2>(response);
                //Console.WriteLine("Choose a forecast type");
                //int foreint = Convert.ToInt32(Console.ReadKey());
                var weatherdata = rescpt.daily.data[daychoice];
                Console.WriteLine(weatherdata.icon);
                Console.WriteLine(weatherdata.summary);
                Console.WriteLine($"Min: {weatherdata.temperatureLow}°C at {DateTimeOffset.FromUnixTimeSeconds(weatherdata.temperatureLowTime)}");
                Console.WriteLine($"Max: {weatherdata.temperatureMax}°C at {DateTimeOffset.FromUnixTimeSeconds(weatherdata.temperatureMaxTime)}");
                Console.WriteLine($"Humidity : "+weatherdata.humidity*100+"%");
            }
            
                Console.ReadKey();
        }
    }
}
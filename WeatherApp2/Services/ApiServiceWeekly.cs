using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeeklyWeather.Models;

namespace WeeklyWeather.Services
{
    public static class ApiServiceWeekly
    {
        public static async Task<Root> GetWeeklyWeather(double latitude, double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&units=metric&cnt=10&appid=b980dde0248e56e1a1de4397ea2ba249", latitude, longitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }

    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Cloudy
{
    public class DataManager
    {
        public static async Task<WeatherData> GetWeather(double latitude, double longitude)
        {
            var weatherData = new WeatherData();

            if (Reachability.IsHostReachable(DarkSky_API.WebApi()))
            {
                string queryString = DarkSky_API.AuthenticatedBaseURL()
                        + latitude.ToString() + "," + longitude.ToString();

                var response = await GetDataFromService(queryString).ConfigureAwait(false);

                weatherData.Time = Conversions.ConvertFromUnixTimeToLocalTime(response.currently.time);
                TimeZoneInfo timezone;
				try
                {
                    timezone = TimeZoneInfo.FindSystemTimeZoneById(response.timezone);
                    weatherData.TimezoneTime = Conversions.ConvertFromUnixTimeToTimeZoneTime(response.currently.time, timezone);
					weatherData.TimezoneIsValid = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception {0}", ex);
                    Debug.WriteLine(String.Format("Failed to find timezone {0}.", response.timezone));
                    weatherData.TimezoneIsValid = false;
                }

                weatherData.Summary = response.currently.summary;
                weatherData.Latitude = response.latitude;
                weatherData.Longitude = response.longitude;
                weatherData.WindSpeed = response.currently.windSpeed;
                weatherData.Temperature = response.currently.temperature;
                weatherData.Humidity = (int)(response.currently.humidity * 100.0);
                weatherData.Icon = response.currently.icon;
                weatherData.TimeZone = response.timezone;
                weatherData.UnixTime = response.currently.time;

                var dailyWeatherForecast = response.daily.data;
                var weatherDayDataList = new List<WeatherDayData>();
                foreach (var item in dailyWeatherForecast)
                {
                    WeatherDayData weatherDayData = new WeatherDayData();
                    weatherDayData.Time = Conversions.ConvertFromUnixTimeToLocalTime(item.time);
                    weatherDayData.Icon = item.icon;
                    weatherDayData.WindSpeed = item.windSpeed;
                    weatherDayData.TemperatureMin = item.temperatureMin;
                    weatherDayData.TemperatureMax = item.temperatureMax;
                    weatherDayData.Humidity = (int)(item.humidity * 100.0);
                    weatherDayDataList.Add(weatherDayData);
                }
                weatherData.DailyData = weatherDayDataList;
            }
            return weatherData;
        }

        private static async Task<ForecastIOResponse> GetDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();

            var response = new HttpResponseMessage();
            var weatherData = new ForecastIOResponse();

            try
            {
                response = await client.GetAsync(queryString);
            }
            catch (WebException e)
            {
                Console.WriteLine("Register failure " + e.Message);
            }

            if (response != null && response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                weatherData = JsonConvert.DeserializeObject<ForecastIOResponse>(json);
            }

            return weatherData;
        }

    }
}

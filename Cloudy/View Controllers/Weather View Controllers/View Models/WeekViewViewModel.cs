using System;
using System.Collections.Generic;
using UIKit;

namespace Cloudy
{
    public struct WeekViewViewModel
    {
		// there is NO database or storage, just an in-memory array
		private WeatherDayData[] dailyData;

   		public WeekViewViewModel(WeatherDayData[] dailyData)
		{
			this.dailyData = dailyData;
		}

        public int NumberOfSections
        {
            get { return 1; }
        }

        public int NumberOfDays
        {
            get
            {
                var count = 0;

                if (dailyData != null)
                    count = dailyData.Length;
                return count;
            }
        }

		public string Day(int index)
        {
            var weatherDayData = dailyData[index];

			var weekDay = weatherDayData.Time.DayOfWeek;
			var weekDayName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)weekDay];

			return weekDayName;
        }

        public string Date(int index)
        {
            var weatherDayData = dailyData[index];

			var day = weatherDayData.Time.Day;
			var month = weatherDayData.Time.Month;
			var dateString = day.ToString() + " "
				+ System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[(int)month - 1];

            return dateString;
		}

        public string TemperatureRange(int index)
        {
			var weatherDayData = dailyData[index];
            var temperatureRange = string.Empty;

			var tempMin = weatherDayData.TemperatureMin;
			var tempMax = weatherDayData.TemperatureMax;
			if (UserSettings.TemperatureNotationValue == (int)UserSettings.TemperatureNotationEnum.celcius)
			{
				var tMinCelcius = Conversions.ToCelcius(tempMin);
				var tMaxCelcius = Conversions.ToCelcius(tempMax);
				temperatureRange = String.Format("{0:0.0} - {1:0.0} °C", tMinCelcius, tMaxCelcius);
			}
			else
			{
				temperatureRange = String.Format("{0:0.0} - {1:0.0} °F", tempMin, tempMax);
			}

            return temperatureRange;
        }

        public string Humidity(int index)
        {
            var weatherDayData = dailyData[index];

            var humidityString = String.Format("{0} %", weatherDayData.Humidity);
            return humidityString;
        }

        public string WindSpeed(int index)
        {
			var weatherDayData = dailyData[index];
			var windSpeed = string.Empty;

			if (UserSettings.UnitsNotationValue == (int)UserSettings.UnitsNotationEnum.metric)
			{
				var speed = Conversions.ToKmPerHour(weatherDayData.WindSpeed);
				windSpeed = String.Format("Wind: {0:0.0} km/h", speed);
			}
			else
			{
				windSpeed = String.Format("Wind: {0:0.0} MPH", weatherDayData.WindSpeed);
			}

            return windSpeed;
        }

        public UIImage Image(int index)
        {
            var weatherDayData = dailyData[index];

            return AppDelegate.Current.GetImageForIcon(weatherDayData.Icon, big: false);
        }

	}
}

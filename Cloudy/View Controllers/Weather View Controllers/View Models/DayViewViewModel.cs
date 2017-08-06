using System;
using System.Globalization;
using UIKit;

namespace Cloudy
{
    public struct DayViewViewModel
    {
        // there is NO database or storage, just in-memory data

        private DateTime dateTime;
		private DateTime timezoneDateTime;
        private bool timezoneIsValid;
		private string timeZone;
        private long unixTime;

        private DayOfWeek weekDay;
        private int day, month;
        private string weekDayName;

        private string summary;
        private double temperature;
        private int humidity;
        private double windSpeed;
        private string icon;

        public DayViewViewModel(WeatherData weatherData)
        {
            this.dateTime = weatherData.Time;
            this.timezoneDateTime = weatherData.TimezoneTime;
			this.timeZone = weatherData.TimeZone;
            this.unixTime = weatherData.UnixTime;
            this.timezoneIsValid = weatherData.TimezoneIsValid;

            this.weekDay = dateTime.DayOfWeek;
            this.month = dateTime.Month;
            this.day = dateTime.Day;
            this.summary = weatherData.Summary;
            this.temperature = weatherData.Temperature;
            this.humidity = weatherData.Humidity;
            this.windSpeed = weatherData.WindSpeed;
            this.icon = weatherData.Icon;

            this.weekDayName = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)weekDay];
        }

        public long UnixTime
        {
            get { return this.unixTime; }
        }

		public string Date
        {
            get
            {
                var dateString = weekDayName
                        + ", " + day.ToString() + " "
                        + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[(int)month - 1];

                return dateString;
            }
        }

        public string Time
        {
			get
			{
                if (DisplayMyLocationTime())
                {
                    var timeString = string.Empty;

                    if (UserSettings.TimeNotationValue == (int)UserSettings.TimeNotationEnum.twentyFourHour)
                    {
                        timeString = dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        timeString = dateTime.ToString("hh:mm:ss", CultureInfo.InvariantCulture);
                    }
                    return timeString;
                }
                else
                {
                    return TimezoneTime;
                }
			}
		}

        public string TimezoneInfo
        {
            get
            {
                if (DisplayMyLocationTime())
                    return string.Empty;
                else
                    return "(local time)";
            }
        }

        private bool DisplayMyLocationTime()
        {
            if (UserSettings.SelectedLocation == UserSettings.DefaultSelectedLocation
                   || string.IsNullOrEmpty(UserSettings.SelectedLocation)
                   || UserSettings.LocalTimeOnValue == (int)UserSettings.LocalTimeNotationEnum.offValue
                   || !timezoneIsValid)
            {
                return true;
            }

            return false;
        }

		public string TimezoneTime
		{
			get
			{
                var timeString = string.Empty;

                if (UserSettings.TimeNotationValue == (int)UserSettings.TimeNotationEnum.twentyFourHour)
                {
                    timeString = timezoneDateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    timeString = timezoneDateTime.ToString("hh:mm:ss", CultureInfo.InvariantCulture);
                }
                return timeString;
			}
		}

		public string TimeZone
		{
			get
			{
				return timeZone;
			}
		}

        public bool TimezoneIsValid()
        {
            return timezoneIsValid;
        }

		public string Summary
        {
            get
            {
                return summary;
            }
        }

        public string Temperature
        {
            get
            {
                var temperatureString = string.Empty;

                if (UserSettings.TemperatureNotationValue == (int)UserSettings.TemperatureNotationEnum.celcius)
				{
					var temp = Conversions.ToCelcius(temperature);
					temperatureString = String.Format("{0:0.0} °C", temp);
				}
				else
				{
					temperatureString = String.Format("{0:0.0} °F", temperature);
				}
                return temperatureString;
            }
        }

        public string Humidity
        {
            get
            {
                var humidityString = String.Format("{0} %", humidity);
                return humidityString;
            }
        }

        public string WindSpeed
        {
            get
            {
                var speedString = string.Empty;

				if (UserSettings.UnitsNotationValue == (int)UserSettings.UnitsNotationEnum.metric)
				{
					var speed = Conversions.ToKmPerHour(windSpeed);
					speedString = String.Format("{0:0.0} km/h", speed);
				}
				else
				{
					speedString = String.Format("{0:0.0} MPH", windSpeed);
				}
                return speedString;
            }
        }

        public UIImage Image
        {
            get
            {
                return AppDelegate.Current.GetImageForIcon(icon, big: true);
            }
        }

        public string CurrentLocation
        {
            get
            {
                return UserSettings.SelectedLocation;
            }
        }

        public bool LocalTimeOn
        {
            get
            {

                return (UserSettings.LocalTimeOnValue == 0 ? false : true);
            }
        }

    }
}

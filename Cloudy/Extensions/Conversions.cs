using Foundation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cloudy
{
    public static class Conversions
    {
        public static double ToCelcius(double value)
        {
            return (value - 32.0) / 1.8;
        }

        public static double ToKmPerHour(double value)
        {
            return value * 1.609344;
        }

        public static NSDate DateTimeToNSDate(this DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }

        public static DateTime NSDateToDateTime(this NSDate date)
        {
            return ((DateTime)date).ToLocalTime();
        }

		public static DateTime ConvertFromUnixTimeToLocalTime(double timestamp)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var localTime = origin.AddSeconds(timestamp).ToLocalTime();
            return localTime;
		}

		public static DateTime ConvertFromUnixTimeToTimeZoneTime(double timestamp, TimeZoneInfo timezone)
		{
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			var universalTime = origin.AddSeconds(timestamp).ToUniversalTime();
            var timezoneTime = TimeZoneInfo.ConvertTimeFromUtc(universalTime, timezone);

            return timezoneTime;
		}

	}
}

using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UIKit;


namespace Cloudy
{
    public static class UserSettings
    {
        public enum TimeNotationEnum { twelveHour, twentyFourHour };
        public enum UnitsNotationEnum { imperial, metric };
        public enum TemperatureNotationEnum { fahrenheit, celcius };
        public enum LocalTimeNotationEnum { offValue, onValue };

        public enum SectionEnum { time, units, temperature, localTime };

        public static int TimeNotationValue { get; private set; }
        public static int UnitsNotationValue { get; private set; }
        public static int TemperatureNotationValue { get; private set; }
		public static string SelectedLocation { get; set; }
        public static List<Location> Locations { get; private set; }
        public static int LocalTimeOnValue { get; private set; }

		public static string TimeNotation { get; private set; }
        public static string UnitsNotation { get; private set; }
        public static string TemperatureNotation { get; private set; }

        const string timeNotation = "timeNotation";
        const string unitsNotation = "unitsNotation";
        const string temperatureNotation = "temperatureNotation";
        const string selectedLocationKey = "selectedLocation";
		const string locationsKey = "locations";
        const string localTimeKey = "localTimeON";

        public const string DefaultSelectedLocation = "My Location";
        public static double MyLocationLatitude { get; set; }
        public static double MyLocationLongitude { get; set; }

        public static void LoadDefaultValues()
        {
            TimeNotation = NSUserDefaults.StandardUserDefaults.StringForKey(timeNotation);
            if (TimeNotation == null)
            {
                string time = TimeNotationEnum.twentyFourHour.ToString();
                UpdateTimeSetting(time);
            }
            TimeNotationValue = (TimeNotation == TimeNotationEnum.twelveHour.ToString()) ? 0 : 1;

            UnitsNotation = NSUserDefaults.StandardUserDefaults.StringForKey(unitsNotation);
            if (UnitsNotation == null)
            {
                string unit = UnitsNotationEnum.metric.ToString();
                UpdateUnitsSetting(unit);
            }
            UnitsNotationValue = (UnitsNotation == UnitsNotationEnum.imperial.ToString()) ? 0 : 1;

            TemperatureNotation = NSUserDefaults.StandardUserDefaults.StringForKey(temperatureNotation);
            if (TemperatureNotation == null)
            {
                string temperature = TemperatureNotationEnum.celcius.ToString();
                UpdateTemperatureSetting(temperature);
            }
            TemperatureNotationValue = (TemperatureNotation == TemperatureNotationEnum.fahrenheit.ToString()) ? 0 : 1;

            MyLocationLatitude = 0;
            MyLocationLongitude = 0;

            SelectedLocation = NSUserDefaults.StandardUserDefaults.StringForKey(selectedLocationKey);
            if (SelectedLocation == null)
                SelectedLocation = string.Empty;
            Locations = LoadFavorites();

            var localTimeOn = NSUserDefaults.StandardUserDefaults.BoolForKey(localTimeKey);
            LocalTimeOnValue = localTimeOn ? 1 : 0;
        }

        private static void UpdateTimeSetting(string time)
        {
            NSUserDefaults.StandardUserDefaults.SetString(time, timeNotation);
            NSUserDefaults.StandardUserDefaults.Synchronize();
            TimeNotation = NSUserDefaults.StandardUserDefaults.StringForKey(timeNotation);
        }

        private static void UpdateUnitsSetting(string unit)
        {
            NSUserDefaults.StandardUserDefaults.SetString(unit, unitsNotation);
            NSUserDefaults.StandardUserDefaults.Synchronize();
            UnitsNotation = NSUserDefaults.StandardUserDefaults.StringForKey(unitsNotation);
        }

        private static void UpdateTemperatureSetting(string temperature)
        {
            NSUserDefaults.StandardUserDefaults.SetString(temperature, temperatureNotation);
            NSUserDefaults.StandardUserDefaults.Synchronize();
            TemperatureNotation = NSUserDefaults.StandardUserDefaults.StringForKey(temperatureNotation);
        }

        private static void UpdateLocalTimeSetting(bool localTimeOn)
        {
            
			NSUserDefaults.StandardUserDefaults.SetBool(localTimeOn, localTimeKey);
            LocalTimeOnValue = localTimeOn ? 1 : 0;
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}

        public static void UpdateSelectedLocation(string location)
        {
            if (SelectedLocation != location)
            {
                NSUserDefaults.StandardUserDefaults.SetString(location, selectedLocationKey);
                NSUserDefaults.StandardUserDefaults.Synchronize();
                SelectedLocation = NSUserDefaults.StandardUserDefaults.StringForKey(selectedLocationKey);
            }
		}


        public static Location GetFavoriteLocation()
        {
            var location = new Location();
            if (Locations?.Count > 0 && !string.IsNullOrEmpty(SelectedLocation))
            {
                foreach (var loc in Locations)
                {
                    if (loc.name == SelectedLocation)
                    {
                        location = loc;
                        break;
                    }
                }
            }
            return location;
        }

        public static bool IsFavoriteAlreadyInList(string locationName)
        {
            if (string.IsNullOrEmpty(locationName))
                return false;

			var locations = LoadFavorites();
			var favorites = new List<Location>();
			bool found = false;

			foreach (var loc in locations)
			{
				if (loc.name == locationName)
					found = true;
				else
					favorites.Add(loc);
			}
            return found;

		}

        public static void RemoveFavoriteLocation(string locationName)
        {
			if (string.IsNullOrEmpty(locationName))
				return;

			var locations = LoadFavorites();
            var favorites = new List<Location>();
            bool found = false;

            foreach (var loc in locations)
            {
                if (loc.name == locationName)
                    found = true;
                else
                    favorites.Add(loc);
            }
            if (found)
			    SaveFavorites(favorites);
		}

        public static void SaveFavorites(List<Location> locations)
        {
            Locations = locations;
            var jsonString = JsonConvert.SerializeObject(locations);
            NSUserDefaults.StandardUserDefaults.SetString(jsonString, locationsKey);
        }

        public static List<Location> GetFavorites()
        {
            return Locations;
        }

        public static List<Location> LoadFavorites()
        {
            var locations = new List<Location>();
            var jsonString = NSUserDefaults.StandardUserDefaults.StringForKey(locationsKey);
            if (!string.IsNullOrEmpty(jsonString))
                locations = JsonConvert.DeserializeObject<List<Location>>(jsonString);
            return locations;
        }

        public static void ChangeSettingsItem (int section)
        {
            switch (section)
            {
                case (int)UserSettings.SectionEnum.time:
                    string time = string.Empty;
                    if (TimeNotationValue == 0)
                        time = TimeNotationEnum.twentyFourHour.ToString();
                    else
                        time = TimeNotationEnum.twelveHour.ToString();

                    UpdateTimeSetting(time);
                    TimeNotationValue = (TimeNotation == TimeNotationEnum.twelveHour.ToString()) ? 0 : 1;
                    break;

                case (int)UserSettings.SectionEnum.units:
                    string unit = string.Empty;
                    if (UnitsNotationValue == 0)
                        unit = UnitsNotationEnum.metric.ToString();
                    else
                        unit = UnitsNotationEnum.imperial.ToString();

                    UpdateUnitsSetting(unit);
                    UnitsNotationValue = (UnitsNotation == UnitsNotationEnum.imperial.ToString()) ? 0 : 1;
                    break;

                case (int)UserSettings.SectionEnum.temperature:
                    string temperature = string.Empty;
                    if (TemperatureNotationValue == 0)
                        temperature = TemperatureNotationEnum.celcius.ToString();
                    else
                        temperature = TemperatureNotationEnum.fahrenheit.ToString();

                    UpdateTemperatureSetting(temperature);
                    TemperatureNotationValue = (TemperatureNotation == TemperatureNotationEnum.fahrenheit.ToString()) ? 0 : 1;
                    break;

                case (int)UserSettings.SectionEnum.localTime:
                    var localTimeOn = UserSettings.LocalTimeOnValue == 0 ? true : false;
                    UpdateLocalTimeSetting(localTimeOn);
                    break;
            }
        }

    }
}

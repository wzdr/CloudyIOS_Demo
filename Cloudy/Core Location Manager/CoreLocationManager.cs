using CoreLocation;
using System;
using UIKit;

namespace Cloudy
{
    public class CoreLocationManager
    {
        // event for the location changing
        public event EventHandler<LocationUpdatedEventArgs> LocationUpdated;
        private event EventHandler<WeatherDataUpdatedEventArgs> WeatherDataUpdatedHandler;

        public CLLocation currentLocation;
        private static bool busyFlag = false;

        public CoreLocationManager()
        {
            CreateCLManager();
            LocationUpdated += OnCLLocatorPositionChanged;
        }

        private async void OnCLLocatorPositionChanged(object sender, LocationUpdatedEventArgs e)
        {
            if (busyFlag)
                return;

            busyFlag = true;
            var latitude = e.Location.Coordinate.Latitude;
            var longitude = e.Location.Coordinate.Longitude;

            var latStr = String.Format("{0:n8}", latitude);
			var lonStr = String.Format("{0:n8}", longitude);
            Console.WriteLine("CLManager: Latitude = {0}, Longitude = {1}", latStr, lonStr);

            UserSettings.MyLocationLatitude = latitude;
			UserSettings.MyLocationLongitude = longitude;
            UserSettings.SelectedLocation = UserSettings.DefaultSelectedLocation;

            var weatherData = new WeatherData();
            weatherData = await DataManager.GetWeather(latitude, longitude);

            if (weatherData.DailyData != null)
            {
                WeatherDataUpdatedHandler?.Invoke(this, new WeatherDataUpdatedEventArgs(weatherData));
            }
            busyFlag = false;
        }

        // create a location manager to get system location updates to the application
        public CLLocationManager LocMgr { get; private set; }

        private void CreateCLManager()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                InstallLocationServices();
            }
        }

        private void InstallLocationServices()
        {
            if (LocMgr == null && CLLocationManager.LocationServicesEnabled)
            {
                LocMgr = new CLLocationManager();
                LocMgr.DesiredAccuracy = CLLocation.AccuracyThreeKilometers;

                LocMgr.DistanceFilter = 1000.0;
                //only do these settings in iOS9 or higher...
                if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
                {
                    LocMgr.AllowsBackgroundLocationUpdates = true;
                    Console.WriteLine("CreateCLManager: 'AllowsBackgroundLocationUpdates' is enabled");
                }

                // Handle the LocationsUpdated event which is sent with >= iOS6 to indicate
                // our location (position) has changed.  
                if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
                {
                    LocMgr.LocationsUpdated += (sender, e) =>
                    {
                        // fire our custom Location Updated event
                        LocationUpdated(this, new LocationUpdatedEventArgs(e.Locations[e.Locations.Length - 1]));
                    };
                }
                // <= iOS5 used UpdatedLocation which has been deprecated.
                else
                {
                    // This generates a warning.
                    LocMgr.UpdatedLocation += (sender, e) =>
                    {
                        // fire our custom Location Updated event
                        LocationUpdated(this, new LocationUpdatedEventArgs(e.NewLocation));
                    };
                }
                // Get some output from our manager in case of failure
                LocMgr.Failed += (sender, e) =>
                {
                    Console.WriteLine("CLLocationManager failed: {0}", e.Error);
                };

            }
        }

        public void SetWeatherDataUpdatedHandler(EventHandler<WeatherDataUpdatedEventArgs> weatherDataUpdated)
        {
            WeatherDataUpdatedHandler = weatherDataUpdated;
        }

        public bool IsLocationServicesEnabled()
        {
            return CLLocationManager.LocationServicesEnabled;
        }
        public bool IsCurrentLocationAvailable()
        {
            return (currentLocation != null);
        }
        public CLLocation GetCurrentLocation()
        {
            return currentLocation;
        }

        public bool IsLocationAuthorized()
        {
            var status = CLLocationManager.Status;
            if (status == CLAuthorizationStatus.AuthorizedWhenInUse
                 || status == CLAuthorizationStatus.AuthorizedAlways
                 || status == CLAuthorizationStatus.Authorized)
                return true;
            else
                return false;
        }

        public void RequestAlwaysAuthorization()
        {
            InstallLocationServices();
            if (LocMgr != null)
            {
                LocMgr.RequestAlwaysAuthorization();
            }
        }
        public void RequestWhenInUseAuthorization()
        {
            InstallLocationServices();
            if (LocMgr != null)
            {
                LocMgr.RequestWhenInUseAuthorization();
            }
        }


        public void StartLocationUpdates()
        {
            if (CLLocationManager.LocationServicesEnabled)
            {
                if (LocMgr != null)
                {
                    LocMgr.StartUpdatingLocation();
                }
            }
            else
            {
                //Let the user know that they need to enable LocationServices
                Console.WriteLine("Location services not enabled, please enable this in your Settings");
            }
        }

        public void StopLocationUpdates()
        {
            if (LocMgr != null)
            {
                // Stop our location updates
                LocMgr.StopUpdatingLocation();
            }
        }

        public void RequestLocation()
        {
            InstallLocationServices();
            if (LocMgr != null)
            {
                // Request one location update
                LocMgr.RequestLocation();
            }
        }

        private void RegionEntered(object sender, CLRegionEventArgs e)
        {
            Console.WriteLine("Entered region " + e.Region.Identifier);
        }

        private void RegionLeft(object sender, CLRegionEventArgs e)
        {
            Console.WriteLine("Left region " + e.Region.Identifier);
        }
    }

}

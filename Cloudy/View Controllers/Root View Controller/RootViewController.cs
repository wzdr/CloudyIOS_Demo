using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UIKit;
using CoreGraphics;

namespace Cloudy
{
    public partial class RootViewController : UIViewController
    {
        public event EventHandler<WeatherDataUpdatedEventArgs> WeatherDataUpdated;
        private NSObject _didBecomeActiveNotificationObserver;

        static DayViewViewModel dayViewViewModel;
        static WeatherDayData[] weatherDayDataArray;
        static WeekViewViewModel weekViewViewModel;
        static bool appDidBecomeActive = false;

        public RootViewController (IntPtr handle) : base (handle)
        {
            if (AppDelegate.Current.LocationDataAreAvailable())
            {
                Debug.WriteLine("RootViewController: Location Data are not available.");
            }
            else
            {
                Debug.WriteLine("RootViewController: Location Data are available.");
            }

            WeatherDataUpdated += OnWeatherDataUpdated;

            AppDelegate.Current.SetupWeatherViewControllers(WeatherDataUpdated);
            UserSettings.LoadDefaultValues();
        }

        private void OnWeatherDataUpdated(object sender, WeatherDataUpdatedEventArgs e)
        {
            BeginInvokeOnMainThread(() =>
            {
                dayViewViewModel = new DayViewViewModel(e.weatherData);
                weatherDayDataArray = e.weatherData.DailyData.ToArray();
                weekViewViewModel = new WeekViewViewModel(weatherDayDataArray);
                UpdateWeatherUI();
            });
        }

        private void BecomeActiveNotificationCallback(NSNotification notification)
        {
            Debug.WriteLine("RootViewController: Did Become Active notification received.");
            appDidBecomeActive = true;
            GetWeatherData();
        }

        private void UpdateWeatherUI()
        {
            WeekDayLabel.Text = dayViewViewModel.Date;
            TimeLabel.Text = dayViewViewModel.Time;
            TimezoneInfoLabel.Text = dayViewViewModel.TimezoneInfo;
            Temp1Label.Text = dayViewViewModel.Temperature;
            Humid1Label.Text = dayViewViewModel.Humidity;
            Speed1Label.Text = dayViewViewModel.WindSpeed;
            SummaryLabel.Text = dayViewViewModel.Summary;
            IconImage.Image = dayViewViewModel.Image;
            CurrentLocationButton.SetTitle(dayViewViewModel.CurrentLocation, UIControlState.Normal);

            Debug.WriteLine("RootViewController: Weather data UI updated.");
            Debug.WriteLine("Weather Time: {0}, Timezone: {1}, UnixTime: {2}",
                            dayViewViewModel.Time, dayViewViewModel.TimeZone, dayViewViewModel.UnixTime);
            Debug.WriteLine(String.Format("(Weather Timezone Time: {0})", dayViewViewModel.TimezoneTime));

            //update cached Weather Day Data array
            if (weekViewViewModel.NumberOfDays > 0)
            {
                // update table view
                WeekTableView.Source = new RootTableSource(weatherDayDataArray);
                WeekTableView.ReloadData();
                ScrollToRow(0, 0, false);
            }
        }

        private void ScrollToRow(int itemIndex, int sectionIndex, bool animated)
        {
            if (weekViewViewModel.NumberOfDays > 0)
            {
                var indexPath = NSIndexPath.FromItemSection(itemIndex, sectionIndex);
                WeekTableView.ScrollToRow(indexPath, UITableViewScrollPosition.Top, animated);
            }
        }

        [Action("UnwindToRootViewController:")]
        public void UnwindToRootViewController(UIStoryboardSegue segue)
        {
            Console.WriteLine("We've unwinded to Yellow!");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            WeekDayLabel.Text = "loading weather data...";
            StartGpsButton.TouchUpInside += ActionGpsButton;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            if (weekViewViewModel.NumberOfDays > 0)
            {
                UpdateWeatherUI();
                ScrollToRow(0, 0, false);
            }
            _didBecomeActiveNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, BecomeActiveNotificationCallback);

            if (string.IsNullOrEmpty(UserSettings.SelectedLocation)
            || UserSettings.SelectedLocation == UserSettings.DefaultSelectedLocation)
            {
                StartGpsButton.Hidden = false;
            }
            else
            {
                StartGpsButton.Hidden = true;
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            if (_didBecomeActiveNotificationObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_didBecomeActiveNotificationObserver);
            }
        }

        private void ActionGpsButton(object sender, EventArgs e)
        {
            GetWeatherData();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            GetWeatherData();
        }

        private void GetWeatherData()
        {
            if (string.IsNullOrEmpty(UserSettings.SelectedLocation)
                || UserSettings.SelectedLocation == UserSettings.DefaultSelectedLocation)
            {
                if (appDidBecomeActive)
                {
                    AppDelegate.Current.RequestLocation();
                    appDidBecomeActive = false;
                }
                else
                {
                    GetWeatherOfMyLocation();
                }
            }
            else
            {
                GetWeatherOfFavoriteLocation();
            }
        }

        private async void GetWeatherOfFavoriteLocation()
        {
            var location = UserSettings.GetFavoriteLocation();
            if(location.IsInvalid())
                return;
            var weatherData = new WeatherData();
            weatherData = await DataManager.GetWeather(location.latitude, location.longitude);

            if (weatherData.DailyData != null)
            {
                WeatherDataUpdated.Invoke(this, new WeatherDataUpdatedEventArgs(weatherData));
            }
        }

        private void GetWeatherOfMyLocation()
        {
            if (!AppDelegate.Current.IsLocationServicesEnabled())
            {
                //Display Alert
                var okAlertController = UIAlertController.Create("Location services not enabled",
                    "Please enable this in your Settings", UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                PresentViewController(okAlertController, true, null);
            }
            else                //if (!AppDelegate.Current.IsCurrentLocationAvailable())
            {
                var selectedLocation = UserSettings.SelectedLocation;

                AttemptToRequestLocation();   //comes here after app installation
            }
        }

        private void AttemptToRequestLocation()
        {
            if (AppDelegate.Current.IsLocationServicesEnabled())
            {
                if (!AppDelegate.Current.IsLocationAuthorized())
                {
                    // request permission - either always or when in use
                    AppDelegate.Current.RequestWhenInUseAuthorization();
                }
                else
                {
                    //this is only requires, if constantly updating location
                    //AppDelegate.Current.StartCoreLocationUpdates();

                    if (Reachability.IsHostReachable(DarkSky_API.WebApi()))
                    {
                        //one-off request
                        AppDelegate.Current.RequestLocation();
                    }
                    else
                    {
                       //Display Alert
                       var okAlertController = UIAlertController.Create("Not connected to internet.",
                          "Please try again later.", UIAlertControllerStyle.Alert);
                       okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                       PresentViewController(okAlertController, true, null);
                    }
                }
            }
            else
            {
                //Display Alert
                var okAlertController = UIAlertController.Create("Please enable",
                    "Location Services", UIAlertControllerStyle.Alert);
                okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
                PresentViewController(okAlertController, true, null);
            }
        }

    }
}

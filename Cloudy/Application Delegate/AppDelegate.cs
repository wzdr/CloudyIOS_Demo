using CoreLocation;
using Foundation;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UIKit; 

namespace Cloudy
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        static Lazy<AppDelegate> app = new Lazy<AppDelegate>(() => new AppDelegate());

        public static AppDelegate Current
        {
            get { return app.Value; }
        }

        // class-level declarations

        private static CoreLocationManager clManager;
        private static bool IsCLListening = false;

        public override UIWindow Window
        {
            get;
            set;
        }

        private WeatherViewController weatherViewController;

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            clManager = new CoreLocationManager();
            return true;
        }

        public bool IsLocationServicesEnabled()
        {
            return clManager.IsLocationServicesEnabled();
        }
        public bool IsCurrentLocationAvailable()
        {
            return clManager.IsCurrentLocationAvailable();
        }
        public CLLocation GetCurrentLocation()
        {
            return clManager.GetCurrentLocation();
        }

        public bool IsLocationAuthorized()
        {
            if (clManager != null)
            {
                return clManager.IsLocationAuthorized();
            }
            return false;
        }
        public void RequestAlwaysAuthorization()
        {
            if (clManager != null)
                clManager.RequestAlwaysAuthorization();
        }
        public void RequestWhenInUseAuthorization()
        {
            if (clManager != null)
                clManager.RequestWhenInUseAuthorization();
        }

        public void RequestLocation()
        {
            if (clManager != null)
            {
                clManager.RequestLocation();
            }
        }

        public void StartCoreLocationUpdates()
        {
            if (clManager != null)
            {
                clManager.StartLocationUpdates();
                IsCLListening = true;
            }
        }

        public void StopCoreLocationUpdates()
        {
            if (clManager != null && IsCLListening)
            {
                clManager.StopLocationUpdates();
                IsCLListening = false;
            }
        }

        public bool LocationDataAreAvailable()
        {
            return true;
        }

        public void SetupWeatherViewControllers(EventHandler<WeatherDataUpdatedEventArgs> weatherDataUpdated)
        {
            if (clManager != null)
            {
                clManager.SetWeatherDataUpdatedHandler(weatherDataUpdated);
            }
            weatherViewController = new WeatherViewController();
        }

        public UIImage GetImageForIcon(string iconName, bool big)
        {
            var image = new UIImage();
            if (weatherViewController != null)
            {
                image = weatherViewController.GetImageForIcon(iconName, big);
            }
            return image;
        }

        public override void OnResignActivation(UIApplication application)
        {
            // Invoked when the application is about to move from active to inactive state.
            // This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) 
            // or when the user quits the application and it begins the transition to the background state.
            // Games should use this method to pause the game.
        }

        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
        }

        public override void WillEnterForeground(UIApplication application)
        {
            // Called as part of the transiton from background to active state.
            // Here you can undo many of the changes made on entering the background.
        }

        public override void OnActivated(UIApplication application)
        {
            // Restart any tasks that were paused (or not yet started) while the application was inactive. 
            // If the application was previously in the background, optionally refresh the user interface.
            Console.WriteLine("OnActivated called, App is active.");

            //if (IsLocationServicesEnabled() && IsLocationAuthorized())
            //{
            //    requestLocationIsPending = true;
            //}
        }

         public override void WillTerminate(UIApplication application)
        {
            // Called when the application is about to terminate. Save data, if needed. See also DidEnterBackground.
        }


        #region ErrorHandling

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
        }
        #endregion ErrorHandling
    }
}
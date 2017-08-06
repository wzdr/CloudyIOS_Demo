// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Cloudy
{
    [Register ("RootViewController")]
    partial class RootViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton CurrentLocationButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Humid1Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView IconImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton SettingsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Speed1Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StartGpsButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel SummaryLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Temp1Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TimeLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel TimezoneInfoLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView WeatherRootView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel WeekDayLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView WeekTableView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CurrentLocationButton != null) {
                CurrentLocationButton.Dispose ();
                CurrentLocationButton = null;
            }

            if (Humid1Label != null) {
                Humid1Label.Dispose ();
                Humid1Label = null;
            }

            if (IconImage != null) {
                IconImage.Dispose ();
                IconImage = null;
            }

            if (SettingsButton != null) {
                SettingsButton.Dispose ();
                SettingsButton = null;
            }

            if (Speed1Label != null) {
                Speed1Label.Dispose ();
                Speed1Label = null;
            }

            if (StartGpsButton != null) {
                StartGpsButton.Dispose ();
                StartGpsButton = null;
            }

            if (SummaryLabel != null) {
                SummaryLabel.Dispose ();
                SummaryLabel = null;
            }

            if (Temp1Label != null) {
                Temp1Label.Dispose ();
                Temp1Label = null;
            }

            if (TimeLabel != null) {
                TimeLabel.Dispose ();
                TimeLabel = null;
            }

            if (TimezoneInfoLabel != null) {
                TimezoneInfoLabel.Dispose ();
                TimezoneInfoLabel = null;
            }

            if (WeatherRootView != null) {
                WeatherRootView.Dispose ();
                WeatherRootView = null;
            }

            if (WeekDayLabel != null) {
                WeekDayLabel.Dispose ();
                WeekDayLabel = null;
            }

            if (WeekTableView != null) {
                WeekTableView.Dispose ();
                WeekTableView = null;
            }
        }
    }
}
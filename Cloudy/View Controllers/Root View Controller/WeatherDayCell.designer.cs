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
    [Register ("WeatherDayCell")]
    partial class WeatherDayCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DateLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DayLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Humid2Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView IconImage2 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Speed2Label { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Temp2Label { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (DateLabel != null) {
                DateLabel.Dispose ();
                DateLabel = null;
            }

            if (DayLabel != null) {
                DayLabel.Dispose ();
                DayLabel = null;
            }

            if (Humid2Label != null) {
                Humid2Label.Dispose ();
                Humid2Label = null;
            }

            if (IconImage2 != null) {
                IconImage2.Dispose ();
                IconImage2 = null;
            }

            if (Speed2Label != null) {
                Speed2Label.Dispose ();
                Speed2Label = null;
            }

            if (Temp2Label != null) {
                Temp2Label.Dispose ();
                Temp2Label = null;
            }
        }
    }
}
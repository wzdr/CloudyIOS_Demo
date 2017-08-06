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
    [Register ("AddLocationTableViewCell")]
    partial class AddLocationTableViewCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel mainLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel secondaryLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (mainLabel != null) {
                mainLabel.Dispose ();
                mainLabel = null;
            }

            if (secondaryLabel != null) {
                secondaryLabel.Dispose ();
                secondaryLabel = null;
            }
        }
    }
}
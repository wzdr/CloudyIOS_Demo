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
    [Register ("AddLocationViewController")]
    partial class AddLocationViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView AddLocationTableView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchBar searchBar { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIActivityIndicatorView SearchCityIndicator { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AddLocationTableView != null) {
                AddLocationTableView.Dispose ();
                AddLocationTableView = null;
            }

            if (searchBar != null) {
                searchBar.Dispose ();
                searchBar = null;
            }

            if (SearchCityIndicator != null) {
                SearchCityIndicator.Dispose ();
                SearchCityIndicator = null;
            }
        }
    }
}
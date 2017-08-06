using Foundation;
using System;
using UIKit;
using CoreLocation;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Cloudy
{
    public partial class LocationsViewController : UIViewController
    {
        CLLocation currentLocation;
        string currentLocationName;
        List<Location> allLocations;
        List<Location> favorites;
        Location myLocation;

		public LocationsViewController(IntPtr handle) : base(handle)
        {
			SetupAllLocations();
        }

        public async void RowSelectedLocationName(int row)
        {
            var allLocationsArray = allLocations.ToArray();
            if (allLocationsArray.Length > row + 1)
            {
                var selectedLocation = allLocationsArray[row +1];

                UserSettings.UpdateSelectedLocation(selectedLocation.name);
                await Task.Delay(1000);
                DismissViewController(true, null);
            }
        }


		public void RemoveLocationAt(int row)
        {
            if (favorites.Count > row)
            {
                favorites.RemoveAt(row);
                UserSettings.SaveFavorites(favorites);
				allLocations = new List<Location>();
				allLocations.Add(myLocation);
				allLocations.AddRange(favorites);
			}
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

            SetupAllLocations();
			LocationsTableView.Source = new LocationsTableViewSource(allLocations, this);
		}

        private void SetupAllLocations()
        {
			favorites = UserSettings.GetFavorites();
            var favoriteLocationName = string.Empty;

			myLocation = new Location
			{
				name = UserSettings.DefaultSelectedLocation,
				latitude = UserSettings.MyLocationLatitude,
				longitude = UserSettings.MyLocationLongitude
			};
			allLocations = new List<Location>();
            if (UserSettings.SelectedLocation == UserSettings.DefaultSelectedLocation)
                allLocations.Add(myLocation);
            else
            {
                var favoriteLocation = UserSettings.GetFavoriteLocation();
                favoriteLocationName = favoriteLocation.name;
                allLocations.Add(favoriteLocation);
            }

            if (string.IsNullOrEmpty(favoriteLocationName))
            {
                allLocations.AddRange(favorites);
            }
            else
            {
                allLocations.Add(myLocation);
                foreach (var loc in favorites)
                {
                    if (favoriteLocationName != loc.name)
                        allLocations.Add(loc);
                }
            }
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
		}

	}
}
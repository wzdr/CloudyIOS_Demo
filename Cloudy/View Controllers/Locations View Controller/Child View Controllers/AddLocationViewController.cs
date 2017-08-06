using System;
using UIKit;
using CoreLocation;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Cloudy
{
    public partial class AddLocationViewController : UIViewController
    {
        List<Location> favorites;


        public AddLocationViewController(IntPtr handle) : base(handle)
        {
            favorites = UserSettings.LoadFavorites();
			locationListHasChanged = false;
		}

		public bool locationListHasChanged { get; set; }

		public void RemoveLocationAt(int row)
		{
			if (favorites.Count > row)  //add 1 for current location
			{
				favorites.RemoveAt(row);
				UserSettings.SaveFavorites(favorites);
			}
		}

		private async void OnSearchButtonClicked(string text)
		{
			searchBar.ResignFirstResponder();
            SearchCityIndicator.StartAnimating();

			try
            {
                var geocoder = new CLGeocoder();
                var placemarkArray = await geocoder.GeocodeAddressAsync(searchBar.Text);
                if (placemarkArray.Length > 0)
                {
                    var firstPosition = placemarkArray[0];
                    var locationLatitude = firstPosition.Location.Coordinate.Latitude;
                    var locationLongitude = firstPosition.Location.Coordinate.Longitude;
                    var locationName = firstPosition.Name;
                    if (!string.IsNullOrEmpty(locationName))
                    {
                        if (UserSettings.IsFavoriteAlreadyInList(locationName))
                        {
							//Display Alert
							var okAlertController = UIAlertController.Create("City Name",
								"is already in List.", UIAlertControllerStyle.Alert);
							okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
							PresentViewController(okAlertController, true, null);
						}
                        else
                        {
                            var loc = new Location
                            {
                                name = locationName,
                                latitude = locationLatitude,
                                longitude = locationLongitude
                            };

                            InvokeOnMainThread(() =>
                           {
                               favorites.Add(loc);
                               UserSettings.SaveFavorites(favorites);

                            // update table view
                            AddLocationTableView.Source = new AddLocationTableViewSource(favorites, this);
                               AddLocationTableView.ReloadData();
                           });
                        }
					}
                    searchBar.Text = "";
                    searchBar.Placeholder = "Enter a City";
                }
            }
            catch (Exception ex)
            {
				//Display Alert
				var okAlertController = UIAlertController.Create("Could not find location",
					"Please try again", UIAlertControllerStyle.Alert);
				okAlertController.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
				PresentViewController(okAlertController, true, null);    
            }
            SearchCityIndicator.StopAnimating();
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			searchBar.SearchButtonClicked += (sender, e) =>
            {
            	if (!String.IsNullOrEmpty(searchBar.Text))
            	{
            		OnSearchButtonClicked(searchBar.Text);
            	}
            };
		}

        public override void ViewDidAppear(bool animated)
        {
			base.ViewDidAppear(true);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			AddLocationTableView.Source = new AddLocationTableViewSource(favorites, this);
		}

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

        }
    }
}
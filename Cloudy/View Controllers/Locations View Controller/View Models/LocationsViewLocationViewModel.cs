using System;
using Foundation;
using CoreLocation;
using UIKit;


namespace Cloudy
{
    public struct LocationsViewLocationViewModel
    {
        private Location[] locations;

		public LocationsViewLocationViewModel(Location[] items)
		{
			this.locations = items;
		}

        public string LocationName(int row)
        {
            if (locations?.Length > row)
            {
                return locations[row].name;
            }
            else
            {
                return string.Empty;
            }
        }

 		public string MyLocationName ()
        {
			if (locations?.Length >= 1)
			{
				return locations[0].name;
			}
			else
			{
				return string.Empty;
			}
		}

		public string MyLocationAsString()
		{
			if (locations?.Length >= 1)
			{
				var latStr = String.Format("{0:n3}", locations[0].latitude);
				var lonStr = String.Format("{0:n3}", locations[0].longitude);
				return String.Format("{0}, {1}", latStr, lonStr);
			}
			else
			{
				return string.Empty;
			}
		}

    }
}

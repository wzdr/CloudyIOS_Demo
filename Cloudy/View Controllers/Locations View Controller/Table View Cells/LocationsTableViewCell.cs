using Foundation;
using System;
using UIKit;

namespace Cloudy
{
    public partial class LocationsTableViewCell : UITableViewCell
    {
		public static readonly NSString LocationsCellId = new NSString("LocationsCell");

        NSString TableViewCellId;

		protected LocationsTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public LocationsTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
		}

		public void UpdateCell(LocationsViewLocationViewModel viewModel, NSIndexPath indexPath, NSString cellId)
		{
			TableViewCellId = cellId;
			var section = indexPath.Section;
			var row = indexPath.Row;


			if (TableViewCellId == LocationsCellId)
			{
			    switch (section)
                {
                    case (int)LocationsSection.current:
                        mainLabel.Text = viewModel.MyLocationName();
                        secondaryLabel.Text = viewModel.MyLocationAsString();
                        break;

                    case (int)LocationsSection.favorite:
                        mainLabel.Text = viewModel.LocationName(row+1);
                        secondaryLabel.Text = string.Empty;
                        break;
                }
            }
            else
            {
                throw new Exception("unexpected Table View Cell ID");
            }

		}
	}
}
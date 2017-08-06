using Foundation;
using System;
using UIKit;

namespace Cloudy
{
	public partial class AddLocationTableViewCell : UITableViewCell
	{
		public static readonly NSString AddLocationCellId = new NSString("AddLocationCell");

		NSString TableViewCellId;

		protected AddLocationTableViewCell(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public AddLocationTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
		}

		public void UpdateCell(LocationsViewLocationViewModel viewModel, NSIndexPath indexPath, NSString cellId)
		{
			TableViewCellId = cellId;
			var section = indexPath.Section;
			var row = indexPath.Row;

			if (TableViewCellId == AddLocationCellId)
			{
				mainLabel.Text = viewModel.LocationName(row);
				secondaryLabel.Text = "";
			}
			else
			{
				throw new Exception("unexpected Table View Cell ID");
			}

		}
	}
}
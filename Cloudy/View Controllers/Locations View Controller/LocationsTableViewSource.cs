using Foundation;
using System;
using UIKit;
using System.Collections.Generic;


namespace Cloudy
{
    public class LocationsTableViewSource : UITableViewSource
    {
        NSString cellIdentifier = (NSString)"LocationsCell"; // set in the Storyboard
        LocationsViewController locationsViewController;
        List<Location> TableItems;

		LocationsViewLocationViewModel viewModel;

		public LocationsTableViewSource(List<Location> items, LocationsViewController uivc)
        {
            viewModel = new LocationsViewLocationViewModel(items.ToArray());
            locationsViewController = uivc;
            TableItems = items;
        }

		public override nint NumberOfSections(UITableView tableView)
		{
			var nbrOfSecs = Enum.GetNames(typeof(LocationsSection)).Length;
			return nbrOfSecs;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
            if (section == (int)LocationsSection.current)
			    return TableItems.Count > 0 ? 1 : 0;
            return TableItems.Count > 0 ? TableItems.Count - 1 : 0;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell,
			var cell = tableView.DequeueReusableCell(cellIdentifier) as LocationsTableViewCell;
			// now set the properties 
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new LocationsTableViewCell(cellIdentifier);

			//disable to select any row in first section
			if (indexPath.Section == 0)
                cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			cell.UpdateCell(viewModel, indexPath, cellIdentifier);

			return cell;
		}

        public override string TitleForHeader(UITableView tableView, nint section)
        {
  			if (section == (int)LocationsSection.current)
				return "Current Location";
            return "Favourite Locations";
        }

        public override nfloat GetHeightForHeader(UITableView tableView, nint section)
        {
            return 50.0f;
        }

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
            if (indexPath.Section == 0)
                return;

            locationsViewController.RowSelectedLocationName(indexPath.Row);
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
            if (indexPath.Section == 0)
                return;
            
			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Delete:
                    TableItems.RemoveAt(indexPath.Row + 1);
					tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    locationsViewController.RemoveLocationAt(indexPath.Row);
					break;
				case UITableViewCellEditingStyle.None:
					break;
			}
		}

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return indexPath.Section == 0 ? false : true;
        }

        public override bool CanFocusRow(UITableView tableView, NSIndexPath indexPath)
        {
			return indexPath.Section == 0 ? false : true;
		}
	}
}

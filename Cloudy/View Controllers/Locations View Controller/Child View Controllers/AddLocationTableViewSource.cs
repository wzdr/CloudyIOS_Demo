using Foundation;
using System;
using UIKit;
using System.Collections.Generic;


namespace Cloudy
{
    public class AddLocationTableViewSource : UITableViewSource
    {
		NSString cellIdentifier = (NSString)"AddLocationCell"; // set in the Storyboard
        List<Location> TableItems;
        AddLocationViewController addLocationViewController; 

        LocationsViewLocationViewModel viewModel;


		public AddLocationTableViewSource(List<Location> items, AddLocationViewController tvc)
		{
            TableItems = items;
			viewModel = new LocationsViewLocationViewModel(items.ToArray());

			addLocationViewController = tvc;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return TableItems.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// in a Storyboard, Dequeue will ALWAYS return a cell,
			var cell = tableView.DequeueReusableCell(cellIdentifier) as AddLocationTableViewCell;
			// now set the properties 
			// if there are no cells to reuse, create a new one
			if (cell == null)
				cell = new AddLocationTableViewCell(cellIdentifier);

            //disable to select any row
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			cell.UpdateCell(viewModel, indexPath, cellIdentifier);
			return cell;
		}

        public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    TableItems.RemoveAt(indexPath.Row);
                    tableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                    break;
                case UITableViewCellEditingStyle.None:
                    break;
            }
        }

	}
}

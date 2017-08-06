using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace Cloudy
{
    public class RootTableSource : UITableViewSource
    {
        NSString cellIdentifier = (NSString)"daycell"; // set in the Storyboard

        WeekViewViewModel weekViewViewModel;

        public RootTableSource(WeatherDayData[] items)
        {
 			weekViewViewModel = new WeekViewViewModel(items);
        }
        public override nint NumberOfSections(UITableView tableView)
        {
            return weekViewViewModel.NumberOfSections;
        }
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return weekViewViewModel.NumberOfDays;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            // in a Storyboard, Dequeue will ALWAYS return a cell,
            WeatherDayCell cell = tableView.DequeueReusableCell(cellIdentifier) as WeatherDayCell;
            // now set the properties 
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new WeatherDayCell(cellIdentifier);
            
			//disable to select any row
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;

			cell.UpdateCell(weekViewViewModel, indexPath.Row);
            return cell;
        }

    }
}

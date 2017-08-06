using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UIKit;

namespace Cloudy
{
    public class SettingsTableViewSource : UITableViewSource
    {
        private event EventHandler _settingsUpdatedHandler;

        NSString cellIdentifier = (NSString)"settingscell"; // set in the Storyboard

        public SettingsTableViewSource(EventHandler SettingsUpdated)
        {
            _settingsUpdatedHandler = SettingsUpdated;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            var nbrOfSecs = Enum.GetNames(typeof(UserSettings.SectionEnum)).Length;
            return nbrOfSecs;
        }
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 2;
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var section = indexPath.Section;
            var row = indexPath.Row;

            // in a Storyboard, Dequeue will ALWAYS return a cell,
            SettingsTableViewCell cell = tableView.DequeueReusableCell(cellIdentifier) as SettingsTableViewCell;
            // now set the properties 
            // if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new SettingsTableViewCell(cellIdentifier);

            cell.UpdateCell(section, row);
             
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var section = indexPath.Section;
            UserSettings.ChangeSettingsItem(section);
            _settingsUpdatedHandler?.Invoke(this, new EventArgs());
        }
    }
}

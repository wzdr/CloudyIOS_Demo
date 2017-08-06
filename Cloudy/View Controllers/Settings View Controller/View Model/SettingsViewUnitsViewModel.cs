using System;
using UIKit;


namespace Cloudy
{
    public struct SettingsViewUnitsViewModel
    {
		int currentRow;

		public SettingsViewUnitsViewModel(int row)
		{
			this.currentRow = row;
		}

		public string Text
		{
			get
			{
				return (currentRow == 0) ? "imperial" : "metric";
			}
		}

		public UITableViewCellAccessory AccessoryType
		{
            get
            {
                var accessoryType = (currentRow == UserSettings.UnitsNotationValue) ?
                    UITableViewCellAccessory.Checkmark :
                    UITableViewCellAccessory.None;

                return accessoryType;
            }
		}
    }
}

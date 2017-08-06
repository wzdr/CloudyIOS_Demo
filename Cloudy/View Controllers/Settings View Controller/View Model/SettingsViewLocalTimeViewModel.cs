using System;
using UIKit;


namespace Cloudy
{
    public struct SettingsViewLocalTimeViewModel
    {
		int currentRow;

		public SettingsViewLocalTimeViewModel(int row)
		{
			this.currentRow = row;
		}

		public string Text
		{
			get
			{
				return (currentRow == 0) ? "display current time" : "display local time";
			}
		}

		public UITableViewCellAccessory AccessoryType
		{
			get
			{
				var accessoryType = (currentRow == UserSettings.LocalTimeOnValue) ?
					UITableViewCellAccessory.Checkmark :
					UITableViewCellAccessory.None;

				return accessoryType;
			}
		}
	}
}

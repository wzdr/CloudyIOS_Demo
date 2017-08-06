using System;
using UIKit;


namespace Cloudy
{
    public struct SettingsViewTimeViewModel
    {
        int currentRow;

		public SettingsViewTimeViewModel(int row)
		{
			this.currentRow = row;
		}

		public string Text
        {
            get
            {
				return (currentRow == 0) ? "12 Hour" : "24 Hour";
            }
        }

        public UITableViewCellAccessory AccessoryType
        {
            get
            {
                var accessoryType = (currentRow == UserSettings.TimeNotationValue) ?
                    UITableViewCellAccessory.Checkmark :
                    UITableViewCellAccessory.None;

                return accessoryType;
            }
        }
    }
}

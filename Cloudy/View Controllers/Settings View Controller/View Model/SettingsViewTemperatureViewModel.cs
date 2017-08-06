using System;
using UIKit;


namespace Cloudy
{
    public struct SettingsViewTemperatureViewModel
    {
		int currentRow;

		public SettingsViewTemperatureViewModel(int row)
		{
			this.currentRow = row;
		}

		public string Text
		{
			get
			{
				return (currentRow == 0) ? "Fahrenheit" : "Celcius";
			}
		}

		public UITableViewCellAccessory AccessoryType
		{
            get
            {
                var accessoryType = (currentRow == UserSettings.TemperatureNotationValue) ?
                    UITableViewCellAccessory.Checkmark :
                    UITableViewCellAccessory.None;

                return accessoryType;
            }
		}
    }
}

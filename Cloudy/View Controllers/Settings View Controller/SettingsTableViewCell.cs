using Foundation;
using System;
using UIKit;

namespace Cloudy
{
    public partial class SettingsTableViewCell : UITableViewCell
    {
        public SettingsTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public SettingsTableViewCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
        }

        public void UpdateCell(int section, int row)
        {
            switch (section)
            {
                case (int)UserSettings.SectionEnum.time:
                    var timeViewModel = new SettingsViewTimeViewModel(row);
                    SettingsLabel.Text = timeViewModel.Text;
					this.Accessory = timeViewModel.AccessoryType;
					break;

                case (int)UserSettings.SectionEnum.units:
					var unitsViewModel = new SettingsViewUnitsViewModel(row);
					SettingsLabel.Text = unitsViewModel.Text;
					this.Accessory = unitsViewModel.AccessoryType;
					break;

                case (int)UserSettings.SectionEnum.temperature:
					var temperatureViewModel = new SettingsViewTemperatureViewModel(row);
					SettingsLabel.Text = temperatureViewModel.Text;
                    this.Accessory = temperatureViewModel.AccessoryType;
					break;
                case (int)UserSettings.SectionEnum.localTime:
                    var localTimeViewModel = new SettingsViewLocalTimeViewModel(row);
                    SettingsLabel.Text = localTimeViewModel.Text;
                    this.Accessory = localTimeViewModel.AccessoryType;
                    break;
            }

         }

    }
}
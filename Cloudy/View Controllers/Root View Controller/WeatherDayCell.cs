using Foundation;
using System;
using UIKit;

namespace Cloudy
{
    public partial class WeatherDayCell : UITableViewCell
    {
		public WeatherDayCell (IntPtr handle) : base (handle)
        {
        }

        public WeatherDayCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
        }

        public void UpdateCell(WeekViewViewModel weekViewViewModel, int row)
        {
            IconImage2.Image = weekViewViewModel.Image(row);
            DayLabel.Text = weekViewViewModel.Day(row);
            DateLabel.Text = weekViewViewModel.Date(row);
            Speed2Label.Text = weekViewViewModel.WindSpeed(row);
            Temp2Label.Text = weekViewViewModel.TemperatureRange(row);
            Humid2Label.Text = weekViewViewModel.Humidity(row);
        }

    }
}
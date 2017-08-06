using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Cloudy;

namespace Cloudy
{
    public class WeatherDataUpdatedEventArgs : EventArgs
    {
        public WeatherData weatherData
        {
            get; private set;
        }

        public WeatherDataUpdatedEventArgs(WeatherData weatherData)
        {
            this.weatherData = weatherData;
        }
    }
}
using CoreLocation;
using System;

namespace Cloudy
{
    public sealed class WeatherDataUpdatedEventArgs : EventArgs
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


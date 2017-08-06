using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Cloudy
{
    public static class UserDefaults
    {
        public enum TimeNotation { twelveHour, twentyFourHour };
        public enum UnitsNotation { imperial, metric };
        public enum TemperatureNotation { fahrenheit, celcius };
    }
}
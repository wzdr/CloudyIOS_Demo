using CoreLocation;
using System;

namespace Cloudy
{
    public sealed class LocationUpdatedEventArgs : EventArgs
    {
        public CLLocation Location
        {
            get; private set;
        }
        
        public LocationUpdatedEventArgs(CLLocation location)
        {
            this.Location = location;
        }
    }
}


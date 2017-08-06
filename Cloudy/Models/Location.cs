﻿using System;


namespace Cloudy
{
    public struct Location
    {
		public enum Keys
		{
			name,
			longitude,
			latitude
		}

        public string name { get; set; }
        public double latitude { get; set; }
		public double longitude { get; set; }

        public bool IsInvalid()
        {
            return object.Equals(latitude, default(double));
        }
	}
}
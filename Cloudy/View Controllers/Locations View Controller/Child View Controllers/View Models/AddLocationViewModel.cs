using System;
using CoreLocation;


namespace Cloudy
{
	public struct AddLocationViewModel
	{
		public CLLocation location { get; set; }

		public AddLocationViewModel(CLLocation loc)
		{
			this.location = loc;
		}


	}
}

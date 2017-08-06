using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Cloudy
{
    public class WeatherViewController
    {
        public UIImage GetImageForIcon(string iconName, bool big)
        {
            var imageName = string.Empty;
            var suffix = big ? "@2x.png" : ".png";
            var image = new UIImage();

            switch (iconName)
            {
                case "cloudy":
                case "03d":
                case "03n":
                case "04d":
                case "04n":
                    imageName = "CloudyIcon-512";
                    break;
                case "rain":
                case "09d":
                case "10d":
                case "11d":
                case "09n":
                case "10n":
                case "11n":
                    imageName = "RainIcon-512";
                    break;
                case "snow":
                case "13d":
                case "13n":
                    imageName = "SnowIcon-512";
                    break;
                case "clear-day":
                case "01d":
                    imageName = "ClearDayIcon-512";
                    break;
                case "clear-night":
                case "01n":
                    imageName = "ClearNightIcon-512";
                    break;
                case "fog":
                case "50n":
                case "50d":
                    imageName = "FogIcon-512";
                    break;
                case "sleet":
                    imageName = "SleetIcon-512";
                    break;

                case "partly-cloudy-day":
                case "02d":
                    imageName = "PartlyCloudyIcon-512";
                    break;

                default:
                    imageName = "CloudyIcon-512";
                    break;
            }

            if (!string.IsNullOrEmpty(imageName))
            {
                return UIImage.FromBundle(imageName + suffix);
            }
            return image;

        }
    }
}
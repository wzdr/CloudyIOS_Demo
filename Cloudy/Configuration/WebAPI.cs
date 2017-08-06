
namespace Cloudy
{
    public struct DarkSky_API
    {
        const string APIKey = "---YOUR_OWN_DARK_SKY_KEY---";
        const string webApi = "https://api.darksky.net/forecast/";

        const string authenticatedBaseURL = webApi + APIKey + "/";

        public static string AuthenticatedBaseURL()
        {
            return authenticatedBaseURL;
        }

        public static string WebApi()
        {
            return webApi;
        }
    }

}

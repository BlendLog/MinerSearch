using System.Globalization;
using System.Net.Http;

namespace netlib
{
    public class UpdateChecker
    {
        public static string GetLatestVersion(string endpoint)
        {
            string url;

            if (endpoint == "primary")
                url = "https://msch3295connect.ru/version.txt";
            else
                url = "https://raw.githubusercontent.com/BlendLog/MinerSearch/refs/heads/master/version.txt";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                return client.GetStringAsync(url).Result;
            }
        }


        public static bool IsLatestVersion(string currentVersion, string latestVersion)
        {
            if (currentVersion.Length > latestVersion.Length)
            {
                latestVersion += "0";
            }
            else if (currentVersion.Length < latestVersion.Length)
            {
                currentVersion += "0";
            }

            double currentVersionAsDouble = double.Parse(currentVersion + "0", CultureInfo.InvariantCulture);
            double latestVersionAsDouble = double.Parse(latestVersion + "0", CultureInfo.InvariantCulture);

            return currentVersionAsDouble >= latestVersionAsDouble;
        }
    }
}

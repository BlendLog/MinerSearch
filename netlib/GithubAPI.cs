using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Windows.Forms;

namespace netlib
{
    public class GithubAPI
    {
        public static string GetLatestVersion(string repo)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                var responseBody = client.GetStringAsync($"https://api.github.com/repos/{repo}/releases/latest").Result;
                JObject json = JObject.Parse(responseBody);
                string latestVersion = json["tag_name"]?.ToString();
                return latestVersion;
            }


        }
    }
}

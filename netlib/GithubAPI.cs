using System;
using System.Net.Http;
using System.Windows.Forms;

namespace netlib
{
    public class GithubAPI
    {
        public static string GetLatestVersion()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.ParseAdd("request");
                var remoteVersion = client.GetStringAsync($"https://msch3295connect.ru/version.txt").Result;
                return remoteVersion;
            }
        }
    }
}

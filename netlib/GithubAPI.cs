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
                // GitHub требует User-Agent для всех запросов
                try
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("request");

                    // Используем GetStringAsync и блокируем поток
                    var responseBody = client.GetStringAsync($"https://api.github.com/repos/{repo}/releases/latest").Result;

                    // Парсим ответ
                    JObject json = JObject.Parse(responseBody);

                    // Извлекаем значение tag_name
                    string latestVersion = json["tag_name"]?.ToString();

                    return latestVersion;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }


        }
    }
}

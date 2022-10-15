using System;
using findaround.Configuration;
using System.Reflection;
using Newtonsoft.Json;
using MonkeyCache.FileStore;
using NgrokApi;
using System.Diagnostics;

namespace findaround.Utilities
{
	public static class BackendUtilities
	{
		public static async Task<Uri> GetBaseUrlAsync()
		{
            Assembly assembly = Assembly.GetExecutingAssembly();
            string json = string.Empty;

            using (var stream = assembly.GetManifestResourceStream("findaround.Configuration.ngrokConfig.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }

            var apiKey = JsonConvert.DeserializeObject<NgrokConfig>(json);

            var response = new HttpResponseMessage();

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.ngrok.com/tunnels"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {apiKey.ApiKey}");
                    request.Headers.TryAddWithoutValidation("Ngrok-Version", "2");

                    response = await httpClient.SendAsync(request);
                }
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var start = responseContent.IndexOf("\"public_url\":\"") + "\"public_url\":\"".Length;
            var end = responseContent.IndexOf("ngrok.io") + "ngrok.io".Length;
            var length = end - start;

            var url = responseContent.Substring(start, length);

            return new Uri(url);
		}

		public static HttpClient ProduceHttpClient()
		{
			var handler = new HttpClientHandler()
			{
				ClientCertificateOptions = ClientCertificateOption.Manual,
				UseDefaultCredentials = true
			};

			var client = new HttpClient(handler);
			return client;
		}

		public static void SaveToken(string token)
		{
			if (!string.IsNullOrWhiteSpace(token))
				Barrel.Current.Add("UserToken", token, TimeSpan.FromDays(14));
		}

		public static string GetToken()
		{
			return Barrel.Current.Get<string>("UserToken");
		}
	}
}


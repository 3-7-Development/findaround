using System;
using findaround.Configuration;
using System.Reflection;
using Newtonsoft.Json;
using MonkeyCache.FileStore;
using System.Diagnostics;

namespace findaround.Utilities
{
	public static class BackendUtilities
	{
		public static string GetBaseUrl()
		{
            Assembly assembly = Assembly.GetExecutingAssembly();
            string json = string.Empty;

            using (var stream = assembly.GetManifestResourceStream("findaround.Configuration.ServerConfig.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }

            var config = JsonConvert.DeserializeObject<ServerConnectionConfig>(json);

            return config.Remote;
        }

		public static HttpClient ProduceHttpClient()
		{
			var handler = new HttpClientHandler()
			{
				ClientCertificateOptions = ClientCertificateOption.Manual,
				UseDefaultCredentials = true
			};

            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=Certyfikat SSL, O=home.pl S.A., C=PL"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.BaseAddress = new Uri(GetBaseUrl());
            return client;
		}

		public static void SaveToken(string token)
		{
			if (!string.IsNullOrWhiteSpace(token))
            {
                if (Barrel.Current.Exists("UserToken"))
                    Barrel.Current.Empty("UserToken");

				Barrel.Current.Add("UserToken", token, TimeSpan.FromDays(14));
            }
		}

		public static string GetToken()
		{
			return Barrel.Current.Get<string>("UserToken");
		}
	}
}


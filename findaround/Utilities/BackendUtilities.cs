using System;
using findaround.Configuration;
using System.Reflection;
using Newtonsoft.Json;
using MonkeyCache.FileStore;

namespace findaround.Utilities
{
	public static class BackendUtilities
	{
		public static string GetBaseServerUrl()
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

			var url = config.Remote + config.Port;

			return url;
		}

		public static void SetToken(string token)
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


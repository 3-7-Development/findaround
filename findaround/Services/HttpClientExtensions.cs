using System;
using System.Net.Http.Headers;
using findaround.Utilities;
using MonkeyCache.FileStore;

namespace findaround.Services
{
	public static class HttpClientExtensions
	{
		public static void SetAuthenticationToken(this HttpClient client)
		{
			var token = BackendUtilities.GetToken();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

		public static void SetBaseUrl(this HttpClient client)
		{
			if (client.BaseAddress is null)
			{
                var address = Barrel.Current.Get<string>("BasicURL");
                client.BaseAddress = new Uri(address);
            }			
		}
	}
}


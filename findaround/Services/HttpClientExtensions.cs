using System;
using System.Net.Http.Headers;
using findaround.Utilities;

namespace findaround.Services
{
	public static class HttpClientExtensions
	{
		public static void SetAuthenticationToken(this HttpClient client)
		{
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Utilities.BackendUtilities.GetToken());
        }

		public static async Task SetBaseUrl(this HttpClient client)
		{
			client.BaseAddress = await BackendUtilities.GetBaseUrlAsync();
		}
	}
}


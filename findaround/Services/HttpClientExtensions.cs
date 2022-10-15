using System;
using System.Net.Http.Headers;
using findaround.Utilities;

namespace findaround.Services
{
	public static class HttpClientExtensions
	{
		public static void SetAuthenticationToken(this HttpClient client)
		{
			var token = BackendUtilities.GetToken();
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

		//public static void SetBaseUrl(this HttpClient client)
		//{
		//	client.BaseAddress = BackendUtilities.GetBaseUrlAsync().Result;
		//}
	}
}


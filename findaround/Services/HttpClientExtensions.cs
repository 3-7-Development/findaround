using System;
using System.Net.Http.Headers;

namespace findaround.Services
{
	public static class HttpClientExtensions
	{
		public static void SetAuthenticationToken(this HttpClient client)
		{
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Utilities.BackendUtilities.GetToken());
        }
	}
}


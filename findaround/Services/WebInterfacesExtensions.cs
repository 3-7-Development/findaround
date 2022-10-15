using System;
using System.Text;
using Newtonsoft.Json;

namespace findaround.Services
{
	public static class WebInterfacesExtensions
	{
        public static StringContent GetRequestContent(this IWebService service, object value)
        {
            var json = JsonConvert.SerializeObject(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return content;
        }
    }
}


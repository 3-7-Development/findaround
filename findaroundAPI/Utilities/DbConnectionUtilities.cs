using System;
using findaroundAPI.Models;
using Newtonsoft.Json;

namespace findaroundAPI.Utilities
{
	public static class DbConnectionUtilities
	{
		public static DbConnectionConfig GetDbConnectionConfig()
		{
			var path = string.Empty;
			var homefolder = Environment.GetEnvironmentVariable("HOME");

			if (!string.IsNullOrWhiteSpace(homefolder))
				path = $"{homefolder}/findaround/config/json/dbConnectionConfig.json";

			string json;

			using (var reader = new StreamReader(path))
			{
				json = reader.ReadToEnd();
			}

            var config = JsonConvert.DeserializeObject<DbConnectionConfig>(json);

            return config;
        }
	}
}


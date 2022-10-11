using System;
using System.Reflection;
using findaroundAPI.Models;
using Newtonsoft.Json;

namespace findaroundAPI.Utilities
{
	public static class DbConnectionUtilities
	{
        public static string? FilePath { get; set; }

		public static DbConnectionConfig GetDbConnectionConfig()
		{
            if (string.IsNullOrWhiteSpace(FilePath))
                throw new ArgumentException("Invalid .JSON config path");

            Assembly assembly = Assembly.GetExecutingAssembly();
            string json = string.Empty;

            using (var stream = assembly.GetManifestResourceStream(FilePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
            }

            var config = JsonConvert.DeserializeObject<DbConnectionConfig>(json);

            return config;
        }
	}
}


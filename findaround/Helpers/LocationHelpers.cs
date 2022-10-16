using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.Helpers
{
	public static class LocationHelpers
	{
		public static async Task<Location> GetCurrentLocation(IGeolocation geolocation)
		{
			Location location = null;

			try
			{
				location = await geolocation.GetLastKnownLocationAsync();

				if (location is null)
				{
					location = await geolocation.GetLocationAsync(new GeolocationRequest
					{
						DesiredAccuracy = GeolocationAccuracy.High,
						Timeout = TimeSpan.FromSeconds(30)
					});
				}
			}
			catch (Exception e)
			{
				return null;
			}
			

			return location;
		}
	}
}


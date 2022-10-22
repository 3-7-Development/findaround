using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using GeoCoordinatePortable;

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
						DesiredAccuracy = GeolocationAccuracy.Best,
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

		public static async Task<double> GetDistanceToPost(IGeolocation geolocation, PostLocation postLocation)
		{
			var location = await GetCurrentLocation(geolocation);

			if (location is null)
			{
				await Shell.Current.DisplayAlert("Cannot get location", "Please enable location and try again", "OK");
				return 0.00;
			}

			var userCoordinates = new GeoCoordinate(location.Latitude, location.Longitude);

			var postCoordinates = new GeoCoordinate(postLocation.Latitude, postLocation.Longitude);

			var distance = userCoordinates.GetDistanceTo(postCoordinates);

			return distance;
        }
	}
}


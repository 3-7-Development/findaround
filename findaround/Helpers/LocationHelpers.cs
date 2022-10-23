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

        public static double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

        public static async Task<double> GetDistanceToPost(IGeolocation geolocation, PostLocation postLocation)
        {
            var location = await GetCurrentLocation(geolocation);

            //Below is an alert displaying the location.
            //await Shell.Current.DisplayAlert("Title", location.ToString(), "OK");

            //await Shell.Current.DisplayAlert("Title", postLocation.ToString(), "OK");

            if (location is null)
            {
                await Shell.Current.DisplayAlert("Cannot get location", "Please enable location and try again", "OK");
                return 0.00;
            }

            //var userCoordinates = new GeoCoordinate(location.Latitude, location.Longitude);

            //var postCoordinates = new GeoCoordinate(postLocation.Longitude, postLocation.Latitude);

            //await Shell.Current.DisplayAlert("Test", postCoordinates.ToString(), "ok");

            var distance = GetDistance(location.Longitude, location.Latitude, postLocation.Longitude, postLocation.Latitude) / PostsHelpers.ToKm;

            //await Shell.Current.DisplayAlert("Location", distance.ToString(), "ok");



            return distance;
        }
    }
}


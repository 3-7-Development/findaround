using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.Helpers
{
	public static class PostsHelpers
	{
        public static PostMatchingDto MatchingCriteria;

        public static double ToKm = 1000000;

        public static async Task RefreshSearchCriteria(IGeolocation geolocation)
        {
            if (MatchingCriteria is null)
                MatchingCriteria = new PostMatchingDto()
                {
                    Id = null,
                    Title = string.Empty,
                    Description = string.Empty,
                    Status = PostStatus.Null,
                    Category = PostCategory.Spotted,
                    Distance = 5.00 * ToKm,
                    AuthorId = null,
                    AuthorName = string.Empty
                };

            var userLocation = await LocationHelpers.GetCurrentLocation(geolocation);

            if (userLocation is null)
            {
                MatchingCriteria.Location = new PostLocation()
                {
                    Latitude = 19.9623,
                    Longitude = 52.12222
                };

                if (DeviceInfo.DeviceType == DeviceType.Virtual)
                {
                    MatchingCriteria.Distance = 10000 * ToKm;
                    MatchingCriteria.Location = new PostLocation()
                    {
                        Latitude = 19.9623,
                        Longitude = 52.12222
                    };
                }
            }
            else
            {
                MatchingCriteria.Distance = 5.00 * ToKm;
                MatchingCriteria.Location = new PostLocation()
                {
                    Latitude = userLocation.Latitude,
                    Longitude = userLocation.Longitude
                };
            }
        }
    }
}


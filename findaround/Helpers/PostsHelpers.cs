using System;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.Helpers
{
	public static class PostsHelpers
	{
		public static PostMatchingDto MatchingCriteria { get; set; }

        public static double KmConverter = 1000000;

        public static async Task RefreshSearchCriteria(IGeolocation geolocation)
        {
            var userLocation = await LocationHelpers.GetCurrentLocation(geolocation);

            if (userLocation is null)
            {
                PostsHelpers.MatchingCriteria = new PostMatchingDto()
                {
                    Id = null,
                    Title = string.Empty,
                    Description = string.Empty,
                    Status = PostStatus.Null,
                    Category = PostCategory.Spotted,
                    Distance = 1000.00 * KmConverter,
                    Location = new PostLocation()
                    {
                        Latitude = 19.9623,
                        Longitude = 52.12222
                    },
                    AuthorId = null,
                    AuthorName = string.Empty
                };

                if (DeviceInfo.DeviceType == DeviceType.Virtual)
                {
                    PostsHelpers.MatchingCriteria = new PostMatchingDto()
                    {
                        Id = null,
                        Title = string.Empty,
                        Description = string.Empty,
                        Status = PostStatus.Null,
                        Category = PostCategory.Spotted,
                        Distance = 40000.00 * KmConverter,
                        Location = new PostLocation()
                        {
                            Latitude = 0.00,
                            Longitude = 0.00
                        },
                        AuthorId = null,
                        AuthorName = string.Empty
                    };
                }
            }
            else
            {
                PostsHelpers.MatchingCriteria = new PostMatchingDto()
                {
                    Id = null,
                    Title = string.Empty,
                    Description = string.Empty,
                    Status = PostStatus.Null,
                    Category = PostCategory.Lost,
                    Distance = 5.00 * KmConverter,
                    Location = new PostLocation()
                    {
                        Latitude = userLocation.Longitude,
                        Longitude = userLocation.Latitude
                    },
                    AuthorId = null,
                    AuthorName = string.Empty
                };
            }
        }
    }
}


using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Helpers;
using findaroundShared.Models;
using Newtonsoft.Json;

namespace findaround.ViewModels
{
	public partial class PostDetailsPageViewModel : ViewModelBase
	{
        readonly IPostService _postService;
		readonly IGeolocation _geolocation;
		readonly IMap _map;

		Post post;
		public Post Post { get => post; set => SetProperty(ref post, value); }

		List<Comment> comments;
		public List<Comment> Comments { get => comments; set => SetProperty(ref comments, value); }

		public PostDetailsPageViewModel(IPostService postService, IGeolocation geolocation, IMap map)
		{
			_postService = postService;
			_geolocation = geolocation;
			_map = map;

            Title = "PostDetailsPage";
        }

		[RelayCommand]
		private async Task Appearing()
		{
            Post = PostsHelpers.SelectedPost;
            var distance = await LocationHelpers.GetDistanceToPost(_geolocation, Post.Location) / PostsHelpers.ToKm;
            Post.DistanceFromUser = Math.Round(distance, 2);
			Post.HasImages = Post.Images.Count > 0;

			Comments = await _postService.GetPostComments(Post.Id);
        }

		[RelayCommand]
		async Task SeeOnMap()
		{
			try
			{
				await _map.OpenAsync(Post.Location.Longitude, Post.Location.Latitude, new MapLaunchOptions
				{
					Name = Post.Title,
					NavigationMode = NavigationMode.None
				});
			}
			catch (Exception e)
			{
				await Shell.Current.DisplayAlert("Cannot open map", "Check if your Maps app is working", "OK");
			}
		}
	}
}


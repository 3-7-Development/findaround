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

		string userComment;
		public string UserComment { get => userComment; set => SetProperty(ref userComment, value); }

		public PostDetailsPageViewModel(IPostService postService, IGeolocation geolocation, IMap map)
		{
			_postService = postService;
			_geolocation = geolocation;
			_map = map;

            Title = "PostDetailsPage";

			UserComment = string.Empty;
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
				await _map.OpenAsync(Post.Location.Latitude, Post.Location.Longitude, new MapLaunchOptions
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

		[RelayCommand]
		async Task AddComment()
		{
			IsBusy = true;

			if (string.IsNullOrWhiteSpace(UserComment))
			{
				await Shell.Current.DisplayAlert("Empty comment", "Cannot add empty comment", "OK");
				IsBusy = false;
			}
			else
			{
				var comment = new Comment()
				{
					Content = UserComment,
					AuthorId = UserHelpers.CurrentUser.Id,
					AuthorName = UserHelpers.CurrentUser.Login,
					PostId = Post.Id
				};

				var isAdded = await _postService.AddPostComment(comment);

				if (!isAdded)
				{
					await Shell.Current.DisplayAlert("Cannot add comment", "Please try again", "OK");
					IsBusy = false;
				}
				else
				{
					UserComment = string.Empty;
					Comments = await _postService.GetPostComments(Post.Id);
					IsBusy = false;
				}
			}
		}
	}
}


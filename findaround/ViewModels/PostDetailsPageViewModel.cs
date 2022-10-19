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

		public Post Post { get; set; }

		List<Comment> comments;
		public List<Comment> Comments { get => comments; set => SetProperty(ref comments, value); }

		public PostDetailsPageViewModel(IPostService postService, IGeolocation geolocation)
		{
			_postService = postService;
			_geolocation = geolocation;

            Title = "PostDetailsPage";

			Post = PostsHelpers.SelectedPost;
		}

		[RelayCommand]
		private async Task Appearing()
		{
            var distance = await LocationHelpers.GetDistanceToPost(_geolocation, Post.Location) / PostsHelpers.ToKm;
            Post.DistanceFromUser = Math.Round(distance, 2);
			Post.HasImages = Post.Images.Count > 0;

			Comments = await _postService.GetPostComments(Post.Id);
        }
	}
}


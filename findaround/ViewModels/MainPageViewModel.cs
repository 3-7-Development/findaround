using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Helpers;
using findaround.Services;
using findaround.Views;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;
using System.Linq;

namespace findaround.ViewModels
{
	[QueryProperty(nameof(Self), nameof(Self))]
	public partial class MainPageViewModel : ViewModelBase
	{
		readonly IUserService _userService;
		readonly IPostService _postService;
		readonly IGeolocation _geolocation;

		string self;
		public string Self { get => self; set => SetProperty(ref self, value); }

		List<Post> posts;
		public List<Post> Posts { get => posts; set => SetProperty(ref posts, value); }

		Post selectedPost;
		public Post SelectedPost { get => selectedPost; set => SetProperty(ref selectedPost, value); }
		
		public MainPageViewModel(IUserService userService, IPostService postService, IGeolocation geolocation)
		{
			_userService = userService;
			_postService = postService;
			_geolocation = geolocation;

			Title = "MainPage";

			Posts = new List<Post>();

			SelectedPost = new Post();

			Self = "false";
        }

		[RelayCommand]
		async Task OnAppearing()
		{
			IsBusy = true;

            await PostsHelpers.RefreshSearchCriteria(_geolocation);

            bool.TryParse(Self, out var getYourPosts);

			if (getYourPosts)
			{
                Posts = await _postService.GetUserPosts();
            }
			else
			{
                Posts = await _postService.MatchPosts(PostsHelpers.MatchingCriteria);
            }

            var userLocation = LocationHelpers.GetCurrentLocation(_geolocation);

            foreach (var post in Posts)
            {
                var distance = await LocationHelpers.GetDistanceToPost(_geolocation, post.Location);
                post.DistanceFromUser = Math.Round(distance, 2);
				post.HasImages = true;
				post.Images = new List<PostImage>() {
					new PostImage()
					{
						Path = "default_post_image.png"
					},
                    new PostImage()
                    {
                        Path = "default_post_image.png"
                    }
                };
            }

			Posts = Posts.OrderBy(p => p.DistanceFromUser).ToList();

            IsBusy = false;
        }

		[RelayCommand]
		async Task CreateNewPost()
		{
			await Shell.Current.GoToAsync(nameof(NewPostPage));
		}

		[RelayCommand]
		async Task GoToSearchPage()
		{
			await Shell.Current.GoToAsync($"{nameof(SearchPostPage)}");
		}

		[RelayCommand]
		async Task GoToProfile()
		{
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

		[RelayCommand]
		async Task GoToCategories()
		{
			await Shell.Current.GoToAsync(nameof(CategoriesPage));
		}

		[RelayCommand]
		async Task Logout()
		{
			IsBusy = true;

			var isLoggedOut = await _userService.LogOutUser();

			if (!isLoggedOut)
			{
				await Shell.Current.DisplayAlert("Cannot log out", "Please try again", "OK");
				IsBusy = false;
			}
			else
			{
				IsBusy = false;
				await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");
			}
		}

		[RelayCommand]
        async Task GoToPostDetails(object args)
		{
			var post = args as Post;

			if (post is null)
				return;

			selectedPost = null;

			PostsHelpers.SelectedPost = post;

			await Shell.Current.GoToAsync($"{nameof(PostDetailsPage)}");
		}

        [RelayCommand]
		async Task GoBack()
		{
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}?Self={false}");
		}
	}

}


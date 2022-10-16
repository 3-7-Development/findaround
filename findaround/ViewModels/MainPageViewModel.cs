using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Helpers;
using findaround.Services;
using findaround.Views;
using findaroundShared.Models;
using findaroundShared.Models.Dtos;

namespace findaround.ViewModels
{
	public partial class MainPageViewModel : ViewModelBase
	{
		readonly IUserService _userService;
		readonly IPostService _postService;
		readonly IGeolocation _geolocation;

		List<Post> posts;
		public List<Post> Posts { get => posts; set => SetProperty(ref posts, value); }
		
		public MainPageViewModel(IUserService userService, IPostService postService, IGeolocation geolocation)
		{
			_userService = userService;
			_postService = postService;
			_geolocation = geolocation;

			Title = "MainPage";

			Posts = new List<Post>();

			
        }

		[RelayCommand]
		async Task OnAppearing()
		{
			IsBusy = true;

			await PostsHelpers.RefreshSearchCriteria(_geolocation);

			await Refresh();

			IsBusy = false;
        }

		[RelayCommand]
		async Task Refresh()
		{
			IsBusy = true;

			Posts = await _postService.MatchPosts(PostsHelpers.MatchingCriteria);

			IsBusy = false;
        }

		[RelayCommand]
		async Task CreateNewPost()
		{
			await Shell.Current.GoToAsync(nameof(NewPostPage));
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
		async Task NextPage()
		{
			await Shell.Current.GoToAsync(nameof(RegisterPage));
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
				await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
			}
		}

		[RelayCommand]
		async Task GoBack()
		{
			await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
		}
	}

}


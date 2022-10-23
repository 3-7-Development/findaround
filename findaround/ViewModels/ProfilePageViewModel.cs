using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaroundShared.Models;

namespace findaround.ViewModels
{
	public partial class ProfilePageViewModel : ViewModelBase
	{
		readonly IUserService _userService;
		readonly IPostService _postService;

		private User user;
		public User User { get => user; set => SetProperty(ref user, value); }

		public ProfilePageViewModel(IUserService userService, IPostService postService)
		{
			_userService = userService;
			_postService = postService;

			Title = "ProfilePage";
		}

		[RelayCommand]
		async Task Appearing()
		{
			IsBusy = true;

			User = await _userService.GetBasicInfoAboutYourself();

			IsBusy = false;
		}

		[RelayCommand]
		async Task LogoutUser()
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
                await Shell.Current.GoToAsync($"///{nameof(Views.LoginPage)}");
            }
        }

		[RelayCommand]
		async Task SeeYourPosts()
		{
			await Shell.Current.GoToAsync($"///{nameof(Views.MainPage)}?Self={true}");
		}
	}
}


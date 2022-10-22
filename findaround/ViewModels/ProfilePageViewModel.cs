using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;

namespace findaround.ViewModels
{
	public partial class ProfilePageViewModel : ViewModelBase
	{
		readonly IUserService _userService;
		readonly IPostService _postService;

		public ProfilePageViewModel(IUserService userService, IPostService postService)
		{
			_userService = userService;
			_postService = postService;

			Title = "ProfilePage";
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
	}
}


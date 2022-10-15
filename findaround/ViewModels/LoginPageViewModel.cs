using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Views;
using findaround.Utilities;
using findaround.Helpers;
using findaroundShared.Models.Dtos;
using MonkeyCache.FileStore;

namespace findaround.ViewModels
{
	public partial class LoginPageViewModel : ViewModelBase
	{
		readonly IUserService _userService;

		string login;
		public string Login { get => login; set => SetProperty(ref login, value); }

        string password;
        public string Password { get => password; set => SetProperty(ref password, value); }

        public LoginPageViewModel(IUserService userService)
		{
			_userService = userService;

			Title = "LoginPage";
		}

		[RelayCommand]
		async void Refresh()
		{
			IsBusy = true;
			ResetInputData();
			var url = await BackendUtilities.GetBaseUrlAsync();
			Barrel.Current.Add("BasicURL", url, TimeSpan.FromDays(7));
			IsBusy = false;
		}

		[RelayCommand]
		async Task LogIn()
		{
			IsBusy = true;

			if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
			{
				await Shell.Current.DisplayAlert("Invalid Login or Password", "Please try again", "OK");
				IsBusy = false;
			}
			else
			{
                var loginModel = new LoginUserDto()
                {
                    Login = Login,
                    Password = Password,
                    ConfirmedPassword = Password
                };

                var isLoggedIn = await _userService.LogInUser(loginModel);

                if (!isLoggedIn)
                {
                    await Shell.Current.DisplayAlert("Cannot log in", "Check your Internet connection and try again", "OK");
                    ResetInputData();
					IsBusy = false;
                }
                else
                {
					var user = await _userService.GetBasicInfoAboutYourself();
                    UserHelpers.CurrentUser = user;

					IsBusy = false;
                    await Shell.Current.GoToAsync($"///{nameof(MainPage)}");
                }
            }
		}

		void ResetInputData()
		{
			Login = string.Empty;
			Password = string.Empty;
		}
	}
}


using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Views;
using findaroundShared.Models.Dtos;

namespace findaround.ViewModels
{
    public partial class RegisterPageViewModel : ViewModelBase
    {
        readonly IUserService _userService;

        string login;
        public string Login { get => login; set => SetProperty(ref login, value); }

        string password;
        public string Password { get => password; set => SetProperty(ref password, value); }

        string confirmedPassword;
        public string ConfirmedPassword { get => confirmedPassword; set => SetProperty(ref confirmedPassword, value); }

        public RegisterPageViewModel(IUserService userService)
        {
            IsBusy = true;

            _userService = userService;

            Title = "Create account";

            ResetInputData();

            IsBusy = false;
        }

        [RelayCommand]
        async Task Register()
        {
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(Login))
            {
                await Shell.Current.DisplayAlert("Error", "Login cannot be empty", "OK");
                IsBusy = false;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmedPassword) ||
                    Password != ConfirmedPassword)
                {
                    await Shell.Current.DisplayAlert("Error", "Check your passwords", "OK");
                    IsBusy = false;
                }
                else
                {
                    var model = new RegisterUserDto()
                    {
                        Login = Login,
                        Password = Password,
                        ConfirmedPassword = ConfirmedPassword
                    };

                    var isRegistered = await _userService.RegisterUser(model);

                    if (!isRegistered)
                    {
                        await Shell.Current.DisplayAlert("Something went wrong", "Couldn't registered new account", "OK");
                        IsBusy = false;
                    }
                    else
                    {
                        IsBusy = false;
                        await Shell.Current.GoToAsync($"///{nameof(LoginPage)}?Login={Login}&Password={Password}&Autologin={true}");
                    }
                }
            }
        }

        private void ResetInputData()
        {
            Login = string.Empty;
            Password = string.Empty;
            ConfirmedPassword = string.Empty;
        }
    }
}


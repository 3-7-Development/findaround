using System;
using findaround.Services;

namespace findaround.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        readonly IUserService _userService;

        public RegisterPageViewModel(IUserService userService)
        {
            _userService = userService;

            Title = "RegisterPage";
        }
    }
}


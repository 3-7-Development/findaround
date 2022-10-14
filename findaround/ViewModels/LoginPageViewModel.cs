using System;
using findaround.Services;

namespace findaround.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
		readonly IUserService _userService;

		public LoginPageViewModel(IUserService userService)
		{
			_userService = userService;

			Title = "LoginPage";
		}
	}
}


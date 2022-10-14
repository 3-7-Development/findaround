using System;
using findaround.Services;

namespace findaround.ViewModels
{
	public class ContactsPageViewModel : ViewModelBase
	{
		readonly IUserService _userService;

		public ContactsPageViewModel(IUserService userService)
		{
			_userService = userService;

			Title = "ContactsPage";
		}
	}
}


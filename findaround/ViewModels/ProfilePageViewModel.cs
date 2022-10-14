using System;
using findaround.Services;

namespace findaround.ViewModels
{
	public class ProfilePageViewModel : ViewModelBase
	{
		readonly IUserService _userService;
		readonly IPostService _postService;

		public ProfilePageViewModel(IUserService userService, IPostService postService)
		{
			_userService = userService;
			_postService = postService;

			Title = "ProfilePage";
		}
	}
}


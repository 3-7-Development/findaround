using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Views;

namespace findaround.ViewModels
{
	public partial class NewPostPageViewModel : ViewModelBase
	{
		readonly IPostService _postService;

		public NewPostPageViewModel(IPostService postService)
		{
			_postService = postService;

			Title = "NewPostPage";
		}
    }
}


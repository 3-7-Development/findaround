using System;
using findaround.Services;

namespace findaround.ViewModels
{
	public class NewPostPageViewModel : ViewModelBase
	{
		readonly IPostService _postService;

		public NewPostPageViewModel(IPostService postService)
		{
			_postService = postService;

			Title = "NewPostPage";
		}
	}
}


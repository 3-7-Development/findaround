using System;
using findaround.Services;

namespace findaround.ViewModels
{
	public class PostDetailsPageViewModel : ViewModelBase
	{
        readonly IPostService _postService;

		public PostDetailsPageViewModel(IPostService postService)
		{
			_postService = postService;

			Title = "PostDetailsPage";
		}
	}
}


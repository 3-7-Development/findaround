using System;
using findaround.Services;

namespace findaround.ViewModels
{
	public class CategoriesPageViewModel : ViewModelBase
	{
		readonly IPostService _postService;

		public CategoriesPageViewModel(IPostService postService)
		{
			_postService = postService;

			Title = "CategoriesPage";
		}
	}
}


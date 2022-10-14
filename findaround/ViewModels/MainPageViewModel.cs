using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Views;
using findaroundShared.Models;

namespace findaround.ViewModels
{
	public partial class MainPageViewModel : ViewModelBase
	{
		readonly IPostService _postService;

		List<Post> posts;
		public List<Post> Posts { get => posts; set => SetProperty(ref posts, value); }

		public MainPageViewModel(IPostService postService)
		{
			_postService = postService;

			Title = "MainPage";

			Posts = new List<Post>();
		}

		[RelayCommand]
		async Task Refresh()
		{
			Posts = await _postService.GetUserPosts(1);
		}

		[RelayCommand]
		async Task CreateNewPost()
		{
			await Shell.Current.GoToAsync(nameof(NewPostPage));
		}

		[RelayCommand]
		async Task GoToProfile()
		{
			await Shell.Current.GoToAsync(nameof(ProfilePage));
		}

		[RelayCommand]
		async Task GoToCategories()
		{
			await Shell.Current.GoToAsync(nameof(CategoriesPage));
		}
	}
}


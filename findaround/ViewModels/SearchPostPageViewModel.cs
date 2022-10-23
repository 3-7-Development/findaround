using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaroundShared.Models;
using findaround.Helpers;

namespace findaround.ViewModels
{
	public partial class SearchPostPageViewModel : ViewModelBase
	{
		IPostService _postService;

		double distanceSlider;
		public double DistanceSlider { get => Math.Round(distanceSlider, 2); set => SetProperty(ref distanceSlider, value); }

		PostCategory selectedCategory;
		public PostCategory SelectedCategory { get => selectedCategory; set => SetProperty(ref selectedCategory, value); }

		public SearchPostPageViewModel(IPostService postService)
		{
			_postService = postService;
		}

		[RelayCommand]
		void Appearing()
		{
            DistanceSlider = PostsHelpers.MatchingCriteria.Distance;
		}

		[RelayCommand]
		async Task UpdateCriteria()
		{
			IsBusy = true;

			PostsHelpers.MatchingCriteria.Distance = DistanceSlider * PostsHelpers.ToKm;
			PostsHelpers.MatchingCriteria.Category = PostCategory.Spotted;

			IsBusy = false;
			await Shell.Current.GoToAsync($"///{nameof(Views.MainPage)}");
		}
	}
}


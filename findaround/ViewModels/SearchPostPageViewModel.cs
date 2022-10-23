using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaroundShared.Models;
using findaroundShared.Helpers;
using findaround.Helpers;

namespace findaround.ViewModels
{
	public partial class SearchPostPageViewModel : ViewModelBase
	{
		IPostService _postService;

		double distanceSlider;
		public double DistanceSlider { get => Math.Round(distanceSlider, 2); set => SetProperty(ref distanceSlider, value); }

		CategoryDisplayModel selectedCategory;
		public CategoryDisplayModel SelectedCategory { get => selectedCategory; set => SetProperty(ref selectedCategory, value); }

		List<CategoryDisplayModel> categories;
		public List<CategoryDisplayModel> Categories { get => categories; set => SetProperty(ref categories, value); }

		public SearchPostPageViewModel(IPostService postService)
		{
			_postService = postService;

			Title = "Search settings";

			Categories = new List<CategoryDisplayModel>();

            foreach (PostCategory category in Enum.GetValues(typeof(PostCategory)))
            {
                Categories.Add(new CategoryDisplayModel()
				{
					Category = category,
					Name = category.ToString(),
					Image = ""
				});
            }
        }

		[RelayCommand]
		void Appearing()
		{
            DistanceSlider = PostsHelpers.MatchingCriteria.Distance / PostsHelpers.ToKm;

			SelectedCategory = new CategoryDisplayModel()
			{
				Category = PostsHelpers.MatchingCriteria.Category,
                Name = PostsHelpers.MatchingCriteria.Category.ToString(),
				Image = ""
            };
        }

		[RelayCommand]
		async Task UpdateCriteria()
		{
			IsBusy = true;

			PostsHelpers.MatchingCriteria.Distance = distanceSlider * PostsHelpers.ToKm;
			PostsHelpers.MatchingCriteria.Category = SelectedCategory.Category;

			IsBusy = false;
			await Shell.Current.GoToAsync($"///{nameof(Views.MainPage)}?Self={false}");
		}
	}
}


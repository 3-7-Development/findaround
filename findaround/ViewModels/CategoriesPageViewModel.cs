using System;
using CommunityToolkit.Mvvm.Input;
using findaround.Services;
using findaround.Helpers;
using findaround.Views;
using findaroundShared.Models;
using findaroundShared.Helpers;

namespace findaround.ViewModels
{
	public partial class CategoriesPageViewModel : ViewModelBase
	{
		PostCategory postCategory;
		public PostCategory PostCategory { get => postCategory; set => SetProperty(ref postCategory, value); }

        CategoryDisplayModel selectedCategory;
		public CategoryDisplayModel SelectedCategory { get => selectedCategory;
			set => SetProperty(ref selectedCategory, value); }

		List<CategoryDisplayModel> postsCategories;
		public List<CategoryDisplayModel> PostsCategories { get => postsCategories; set => SetProperty(ref postsCategories, value); }

		public CategoriesPageViewModel()
		{
			Title = "Categories";

			PostsCategories = new List<CategoryDisplayModel>();

			foreach (PostCategory category in Enum.GetValues(typeof(PostCategory)))
			{
				PostsCategories.Add(new CategoryDisplayModel()
				{
					Category = category,
					Name = category.ToString(),
					Image = category.ToString().ToLower() + "_image.png"
				});
			}
		}

		private string GetCategoryImage(PostCategory category)
		{
			var image = "spotted";

			switch (category)
			{
				case PostCategory.Spotted:
					image = "spotted";
					break;

				case PostCategory.Lost:
					image = "lost";
					break;

				case PostCategory.HelpUkraine:
					image = "helpukraine";
					break;

				case PostCategory.Other:
					image = "other";
					break;
			}

			image += "_image.png";

			return image;
		}

		[RelayCommand]
		void Appearing()
		{
			PostCategory = PostsHelpers.MatchingCriteria.Category;
		}

		[RelayCommand]
		async Task CategorySelected(object args)
		{
			var choosenCategory = args as CategoryDisplayModel;

			if (choosenCategory is null)
				return;

			PostCategory = EnumHelpers.ToPostCategory(selectedCategory.Name);
			PostsHelpers.MatchingCriteria.Category = selectedCategory.Category;

            selectedCategory = null;

            await Shell.Current.GoToAsync($"///{nameof(MainPage)}?Self={false}");
		}
	}
}


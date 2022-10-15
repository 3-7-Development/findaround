using System;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using findaround.Views;

namespace findaround.ViewModels
{
	public partial class ViewModelBase : BaseViewModel
	{
		public ViewModelBase()
		{
		}

        [RelayCommand]
        async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}


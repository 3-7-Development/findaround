using System;
using CommunityToolkit.Mvvm.Input;
using MvvmHelpers;
using findaround.Views;

namespace findaround.ViewModels
{
	public partial class ViewModelBase : BaseViewModel
	{
        bool isBusy;
        public new bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (SetProperty(ref isBusy, value, "IsBusy"))
                {
                    IsNotBusy = !isBusy;
                }
                ButtonBackgroundColor = Colors.GreenYellow;
            }
        }

        Color buttonBackgroundColor;
        public Color ButtonBackgroundColor
        {
            get => buttonBackgroundColor;

            set
            {
                var newColorName = IsBusy ? "ButtonUnavailableColor" : "Primary";
                Application.Current.Resources.TryGetValue(newColorName, out var newColor);
                SetProperty(ref buttonBackgroundColor, (Color)newColor);
            }
        }

		public ViewModelBase()
		{
            ButtonBackgroundColor = Colors.GreenYellow;
		}

        [RelayCommand]
        async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}


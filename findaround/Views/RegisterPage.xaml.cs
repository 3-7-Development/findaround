using findaround.ViewModels;

namespace findaround.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

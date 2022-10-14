namespace findaround.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(ViewModels.LoginPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

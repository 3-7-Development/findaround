using findaround.ViewModels;

namespace findaround.Views;

public partial class NewPostPage : ContentPage
{
	public NewPostPage(NewPostPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

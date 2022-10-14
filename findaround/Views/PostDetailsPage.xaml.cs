using findaround.ViewModels;

namespace findaround.Views;

public partial class PostDetailsPage : ContentPage
{
	public PostDetailsPage(PostDetailsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

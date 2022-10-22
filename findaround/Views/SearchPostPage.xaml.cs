namespace findaround.Views;

public partial class SearchPostPage : ContentPage
{
	public SearchPostPage(ViewModels.SearchPostPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

namespace findaround.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(ViewModels.CategoriesPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

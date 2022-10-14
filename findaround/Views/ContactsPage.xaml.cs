using findaround.ViewModels;

namespace findaround.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage(ContactsPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

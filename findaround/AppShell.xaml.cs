namespace findaround;
using findaround.Views;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        Routing.RegisterRoute(nameof(ContactsPage), typeof(ContactsPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(NewPostPage), typeof(NewPostPage));
        Routing.RegisterRoute(nameof(PostDetailsPage), typeof(PostDetailsPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    }
}


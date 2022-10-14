using CommunityToolkit.Maui;
using findaround.Services;
using findaround.ViewModels;
using findaround.Views;
using MonkeyCache.FileStore;

namespace findaround;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		Barrel.ApplicationId = AppInfo.PackageName;

		// Dependencies

		// Services
		builder.Services.AddSingleton<IUserService, TestUsersService>();
        builder.Services.AddSingleton<IPostService, TestPostsService>();

		// ViewModels
		builder.Services.AddSingleton<RegisterPageViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<CategoriesPageViewModel>();
        builder.Services.AddSingleton<ProfilePageViewModel>();
        builder.Services.AddSingleton<PostDetailsPageViewModel>();
        builder.Services.AddSingleton<NewPostPageViewModel>();
        builder.Services.AddSingleton<ContactsPageViewModel>();

        // Views
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<CategoriesPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<PostDetailsPage>();
        builder.Services.AddSingleton<NewPostPage>();
        builder.Services.AddSingleton<ContactsPage>();

        return builder.Build();
	}
}


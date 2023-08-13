using MauiEFCoreStudy.Constants;
using MauiEFCoreStudy.Services;
using MauiEFCoreStudy.Services.Interfaces;
using MauiEFCoreStudy.ViewModels;
using MauiEFCoreStudy.Views;

namespace MauiEFCoreStudy;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews();

        // https://docs.microsoft.com/ja-jp/dotnet/maui/fundamentals/shell/navigation
        Routing.RegisterRoute(RoutingPath.Main, typeof(MainPage));
        Routing.RegisterRoute(RoutingPath.Author, typeof(AuthorPage));
        Routing.RegisterRoute(RoutingPath.Book, typeof(BookPage));

        return builder.Build();
    }

    /// <summary>
    /// サービスを登録する。
    /// https://learn.microsoft.com/ja-jp/dotnet/architecture/maui/dependency-injection
    /// </summary>
    /// <param name="builder"><see cref="MauiAppBuilder"/></param>
    /// <returns><see cref="MauiAppBuilder"/></returns>
    private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IDialogService, DialogService>();

        return builder;
    }

    /// <summary>
    /// ViewModel を登録する。
    /// https://learn.microsoft.com/ja-jp/dotnet/architecture/maui/dependency-injection
    /// </summary>
    /// <param name="builder"><see cref="MauiAppBuilder"/></param>
    /// <returns><see cref="MauiAppBuilder"/></returns>
    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddTransient<AuthorPageViewModel>();
        builder.Services.AddTransient<BookPageViewModel>();

        return builder;
    }

    /// <summary>
    /// View を登録する。
    /// https://learn.microsoft.com/ja-jp/dotnet/architecture/maui/dependency-injection
    /// </summary>
    /// <param name="builder"><see cref="MauiAppBuilder"/></param>
    /// <returns><see cref="MauiAppBuilder"/></returns>
    private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();

        builder.Services.AddTransient<AuthorPage>();
        builder.Services.AddTransient<BookPage>();

        return builder;
    }
}

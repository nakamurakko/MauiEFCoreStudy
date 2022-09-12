using MauiEFCoreStudy.DB;
using MauiEFCoreStudy.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MauiEFCoreStudy;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        // https://docs.microsoft.com/ja-jp/dotnet/maui/fundamentals/shell/navigation
        Routing.RegisterRoute("Main", typeof(MainPage));
        Routing.RegisterRoute("Book", typeof(BookPage));

        return builder.Build();
    }
}

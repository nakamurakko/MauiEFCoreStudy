using MauiEFCoreStudy.ViewModels;

namespace MauiEFCoreStudy.Views;

public partial class BookPage : ContentPage
{
    /// <summary>
    /// コンストラクター。
    /// https://learn.microsoft.com/ja-jp/dotnet/architecture/maui/dependency-injection
    /// </summary>
    /// <param name="bookPageViewModel"><see cref="BookPageViewModel"/></param>
    public BookPage(BookPageViewModel bookPageViewModel)
    {
        BindingContext = bookPageViewModel;

        InitializeComponent();
    }
}
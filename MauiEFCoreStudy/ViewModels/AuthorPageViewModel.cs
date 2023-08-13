using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.Models;

namespace MauiEFCoreStudy.ViewModels;

/// <summary>
/// AuthorPage 用 ViewModel。
/// </summary>
public partial class AuthorPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "著者を追加";

    [ObservableProperty]
    private Author _author = new Author();

    [RelayCommand]
    private void AddAuthor()
    {
        BookModel.AddAuthor(Author);

        Author = new Author();
    }
}

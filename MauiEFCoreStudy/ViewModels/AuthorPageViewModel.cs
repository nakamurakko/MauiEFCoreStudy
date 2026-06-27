using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.DB.Entities;
using MauiEFCoreStudy.Models;

namespace MauiEFCoreStudy.ViewModels;

/// <summary>
/// AuthorPage 用 ViewModel。
/// </summary>
public partial class AuthorPageViewModel : ObservableObject
{

    [ObservableProperty]
    public partial string Title { get; set; } = "著者を追加";

    [ObservableProperty]
    public partial Author Author { get; set; } = new();

    [RelayCommand]
    private async Task AddAuthor()
    {
        await BookModel.AddAuthorAsync(this.Author);

        this.Author = new Author();
    }

}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.Constants;
using MauiEFCoreStudy.DB.Entities;
using MauiEFCoreStudy.Models;
using MauiEFCoreStudy.ViewModels.Common;
using System.Collections.ObjectModel;

namespace MauiEFCoreStudy.ViewModels;

/// <summary>
/// MainPage 用 ViewModel。
/// </summary>
public partial class MainPageViewModel : ObservableObject, IAsyncInitialization
{

    [ObservableProperty]
    public partial string Title { get; set; } = "MauiEFCoreStudy";

    [ObservableProperty]
    public partial ObservableCollection<Book> Books { get; set; } = new();

    /// <summary>
    /// 検索対象の本のタイトル。
    /// </summary>
    [ObservableProperty]
    public partial string SearchTitle { get; set; } = "";

    public Task Initialization { get; private set; }

    /// <summary>
    /// コンストラクター。
    /// </summary>
    public MainPageViewModel()
    {
        this.Initialization = this.InitializeAsync();
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync()
    {
        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        this.Books = new ObservableCollection<Book>(books);
    }

    /// <summary>
    /// 本の詳細を表示する。
    /// </summary>
    /// <param name="book">本情報。</param>
    [RelayCommand]
    private void ShowBookDetail(Book book)
    {
        Shell.Current.GoToAsync(RoutingPath.Book, new Dictionary<string, object>() { { nameof(Book), book }, { nameof(DisplayMode), DisplayMode.ReadOnly } });
    }

    /// <summary>
    /// 本を検索する。
    /// </summary>
    [RelayCommand]
    private async Task SearchBooksAsync()
    {
        IEnumerable<Book> books = await BookModel.GetBooksAsync(this.SearchTitle);
        this.Books = new ObservableCollection<Book>(books);
    }

    /// <summary>
    /// 検索結果をクリアする。
    /// </summary>
    [RelayCommand]
    private async Task ClearSearchResultAsync()
    {
        this.SearchTitle = "";

        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        this.Books = new ObservableCollection<Book>(books);
    }

}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.Constants;
using MauiEFCoreStudy.DataTypes;
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
    private string _title = "MauiEFCoreStudy";

    [ObservableProperty]
    private ObservableCollection<Book> _books = new ObservableCollection<Book>();

    /// <summary>
    /// 検索対象の本のタイトル。
    /// </summary>
    [ObservableProperty]
    private string _searchTitle = "";

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
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
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
        if (string.IsNullOrWhiteSpace(this.SearchTitle))
        {
            return;
        }

        this.Books.Clear();
        IEnumerable<Book> books = await BookModel.GetBooksAsync(this.SearchTitle);
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
    }

    /// <summary>
    /// 検索結果をクリアする。
    /// </summary>
    [RelayCommand]
    private async Task ClearSearchResultAsync()
    {
        this.SearchTitle = "";

        this.Books.Clear();
        IEnumerable<Book> books = await BookModel.GetBooksAsync();
        foreach (Book book in books)
        {
            this.Books.Add(book);
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.Constants;
using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.Models;
using MauiEFCoreStudy.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Initialization = InitializeAsync();
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync()
    {
        var books = await BookModel.GetBooksAsync();
        foreach (var book in books)
        {
            Books.Add(book);
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
        if (string.IsNullOrWhiteSpace(SearchTitle))
        {
            return;
        }

        Books.Clear();
        var books = await BookModel.GetBooksAsync(SearchTitle);
        foreach (var book in books)
        {
            Books.Add(book);
        }
    }

    /// <summary>
    /// 検索結果をクリアする。
    /// </summary>
    [RelayCommand]
    private async Task ClearSearchResultAsync()
    {
        SearchTitle = "";

        Books.Clear();
        var books = await BookModel.GetBooksAsync();
        foreach (var book in books)
        {
            Books.Add(book);
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.Constants;
using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.Models;
using MauiEFCoreStudy.Services.Interfaces;
using MauiEFCoreStudy.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.ViewModels;

/// <summary>
/// BookPage 用 ViewModel。
/// </summary>
[QueryProperty(nameof(Book), nameof(Book))]
[QueryProperty(nameof(DisplayMode), nameof(DisplayMode))]
public partial class BookPageViewModel : ObservableObject, IAsyncInitialization
{
    /// <summary>
    /// <see cref="IDialogService"/>
    /// </summary>
    private IDialogService _dialogService;

    [ObservableProperty]
    private string _title = "本情報";

    [ObservableProperty]
    private Book _book = new Book();

    [ObservableProperty]
    private DisplayMode _displayMode = DisplayMode.Add;

    [ObservableProperty]
    private bool _isReadonly = false;

    [ObservableProperty]
    private ObservableCollection<Author> _authors = new ObservableCollection<Author>();

    [ObservableProperty]
    private Author _selectedAuthor;

    public Task Initialization { get; private set; }

    /// <summary>
    /// コンストラクター。
    /// </summary>
    /// <param name="dialogService"><see cref="IDialogService"/></param>
    public BookPageViewModel(IDialogService dialogService)
    {
        // https://learn.microsoft.com/ja-jp/dotnet/architecture/maui/dependency-injection
        _dialogService = dialogService;
        
        Initialization = InitializeAsync();
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync()
    {
        var authors = await BookModel.GetAuthorsAsync();
        foreach (var author in authors)
        {
            Authors.Add(author);
        }

        if (!string.IsNullOrEmpty(Book.Title))
        {
            await _dialogService.DisplayAlert("", Book.Title, "OK", "Cancel");
        }
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    [RelayCommand]
    private void AddBook()
    {
        BookModel.AddBook(Book);

        Book = new Book();
        SelectedAuthor = null;
    }

    /// <summary>
    /// Book 変更時処理。
    /// </summary>
    /// <param name="value">本情報。</param>
    /// <remarks>
    /// https://docs.microsoft.com/ja-jp/dotnet/communitytoolkit/mvvm/generators/observableproperty#running-code-upon-changes
    /// </remarks>
    partial void OnBookChanged(Book value)
    {
        if (value == null)
        {
            return;
        }

        if (!value.AuthorId.HasValue)
        {
            return;
        }

        _selectedAuthor = _authors.Where(x => x.AuthorId == value.AuthorId.Value).FirstOrDefault();
    }

    /// <summary>
    /// SelectedAuthor 変更時処理。
    /// </summary>
    /// <param name="value">著者情報。</param>
    partial void OnSelectedAuthorChanged(Author value)
    {
        if (value == null)
        {
            _book.AuthorId = null;
            _book.Author = null;

            return;
        }

        _book.AuthorId = value.AuthorId;
        _book.Author = value;
    }

    /// <summary>
    /// DisplayMode 変更時処理。
    /// </summary>
    /// <param name="value"><see cref="DisplayMode"/></param>
    partial void OnDisplayModeChanged(DisplayMode value)
    {
        switch (value)
        {
            case DisplayMode.Add:
                _title = "本を追加";
                IsReadonly = false;

                break;
            case DisplayMode.Edit:
                _title = "本を編集";
                IsReadonly = false;

                break;
            case DisplayMode.ReadOnly:
                _title = "本情報";
                IsReadonly = true;

                break;
        }
    }
}

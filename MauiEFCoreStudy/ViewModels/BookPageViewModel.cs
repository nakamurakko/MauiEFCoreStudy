using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.Constants;
using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.ViewModels;

/// <summary>
/// BookPage用ViewModel。
/// </summary>
[QueryProperty(nameof(Book), nameof(Book))]
[QueryProperty(nameof(DisplayMode), nameof(DisplayMode))]
public partial class BookPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "本情報";

    [ObservableProperty]
    private Book _book = new Book();

    [ObservableProperty]
    private DisplayMode _displayMode = DisplayMode.Add;

    [ObservableProperty]
    private bool _isReadonly = false;

    [ObservableProperty]
    private ObservableCollection<Author> _authors;

    [ObservableProperty]
    private Author _selectedAuthor;

    /// <summary>
    /// コンストラクター。
    /// </summary>
    public BookPageViewModel()
    {
        _authors = new ObservableCollection<Author>(BookModel.GetAuthors());
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
    /// _book 変更時処理。
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
    /// _selectedAuthor 変更時処理。
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
    /// _displayMode 変更時処理。
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

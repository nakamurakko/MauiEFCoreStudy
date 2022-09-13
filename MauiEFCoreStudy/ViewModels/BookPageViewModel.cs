using CommunityToolkit.Mvvm.ComponentModel;
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
public partial class BookPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "本追加";

    [ObservableProperty]
    private Book _book = new Book();

    [ObservableProperty]
    private ObservableCollection<Author> _authors;

    [ObservableProperty]
    private Author _selectedAuthor;

    /// <summary>
    /// 本を追加するCommand。
    /// </summary>
    public Command AddBookCommand { get; set; }

    /// <summary>
    /// コンストラクター。
    /// </summary>
    public BookPageViewModel()
    {
        AddBookCommand = new Command(() => AddBookCommandExecute());

        _authors = new ObservableCollection<Author>(BookModel.GetAuthors());
    }

    private void AddBookCommandExecute()
    {
        BookModel.AddBook(_book);
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
        _book.AuthorId = value.AuthorId;
        _book.Author = value;
    }
}

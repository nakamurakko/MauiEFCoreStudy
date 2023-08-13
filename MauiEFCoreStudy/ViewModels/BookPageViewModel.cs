using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.Constants;
using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.Models;
using MauiEFCoreStudy.Services.Interfaces;
using MauiEFCoreStudy.ViewModels.Common;
using System.Collections.ObjectModel;

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
        this._dialogService = dialogService;

        this.Initialization = this.InitializeAsync();
    }

    /// <summary>
    /// 非同期で初期化する。
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    private async Task InitializeAsync()
    {
        IEnumerable<Author> authors = await BookModel.GetAuthorsAsync();
        foreach (Author author in authors)
        {
            this.Authors.Add(author);
        }

        if (!string.IsNullOrEmpty(this.Book.Title))
        {
            await this._dialogService.DisplayAlert("", this.Book.Title, "OK", "Cancel");
        }
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    [RelayCommand]
    private void AddBook()
    {
        BookModel.AddBook(this.Book);

        this.Book = new Book();
        this.SelectedAuthor = null;
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

        this.SelectedAuthor = this.Authors.Where(x => x.AuthorId == value.AuthorId.Value).FirstOrDefault();
    }

    /// <summary>
    /// SelectedAuthor 変更時処理。
    /// </summary>
    /// <param name="value">著者情報。</param>
    partial void OnSelectedAuthorChanged(Author value)
    {
        if (value == null)
        {
            this.Book.AuthorId = null;
            this.Book.Author = null;

            return;
        }

        this.Book.AuthorId = value.AuthorId;
        this.Book.Author = value;
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
                this.Title = "本を追加";
                this.IsReadonly = false;

                break;
            case DisplayMode.Edit:
                this.Title = "本を編集";
                this.IsReadonly = false;

                break;
            case DisplayMode.ReadOnly:
                this.Title = "本情報";
                this.IsReadonly = true;

                break;
        }
    }
}

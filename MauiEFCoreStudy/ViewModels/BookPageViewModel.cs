using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.Models;
using System;
using System.Collections.Generic;
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
    }

    private void AddBookCommandExecute()
    {
        BookModel.AddBook(_book);
    }
}

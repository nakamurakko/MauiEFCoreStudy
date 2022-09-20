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
/// MainPage用ViewModel。
/// </summary>
public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "MauiEFCoreStudy";

    [ObservableProperty]
    private ObservableCollection<Book> _books = new ObservableCollection<Book>(BookModel.GetBooks());

    /// <summary>
    /// 本の詳細を表示する。
    /// </summary>
    /// <param name="book">本情報。</param>
    [RelayCommand]
    private void ShowBookDetail(Book book)
    {
        Shell.Current.GoToAsync(RoutingPath.Book, new Dictionary<string, object>() { { nameof(Book), book }, { nameof(DisplayMode), DisplayMode.ReadOnly } });
    }
}

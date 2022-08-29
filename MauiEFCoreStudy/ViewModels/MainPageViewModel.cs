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
public class MainPageViewModel
{
    public string Title { get; set; } = "MauiEFCoreStudy";

    public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>(BookModel.GetBooks());

    public MainPageViewModel()
    {

    }
}

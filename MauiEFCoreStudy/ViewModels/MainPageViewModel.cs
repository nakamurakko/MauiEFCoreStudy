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
/// MainPage用ViewModel。
/// </summary>
public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "MauiEFCoreStudy";

    [ObservableProperty]
    private ObservableCollection<Book> _books = new ObservableCollection<Book>(BookModel.GetBooks());
}

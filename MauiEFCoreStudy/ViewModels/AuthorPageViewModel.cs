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
/// AuthorPage 用 ViewModel。
/// </summary>
public partial class AuthorPageViewModel : ObservableObject
{
    [ObservableProperty]
    private Author _author = new Author();

    [RelayCommand]
    private void AddAuthor()
    {
        BookModel.AddAuthor(Author);

        Author = new Author();
    }
}

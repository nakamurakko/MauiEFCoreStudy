using CommunityToolkit.Mvvm.ComponentModel;
using MauiEFCoreStudy.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.ViewModels;

public partial class BookPageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title = "本追加";

    [ObservableProperty]
    private Book _book = new Book();
}

using MauiEFCoreStudy.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.ViewModels;

public class BookPageViewModel
{
    public string Title { get; set; } = "本追加";

    public Book Book { get; set; } = new Book();
}

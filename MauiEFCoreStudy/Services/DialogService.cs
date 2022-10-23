using MauiEFCoreStudy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.Services;

/// <summary>
/// ダイアログメッセージサービス用クラス。
/// </summary>
public class DialogService : IDialogService
{
    public Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }
}

using MauiEFCoreStudy.Services.Interfaces;

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

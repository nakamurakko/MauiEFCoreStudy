using MauiEFCoreStudy.Services.Interfaces;

namespace MauiEFCoreStudy.Services;

/// <summary>
/// ダイアログメッセージサービス用クラス。
/// </summary>
public class DialogService : IDialogService
{

    public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current.Windows[0].Page.DisplayAlertAsync(title, message, accept, cancel);
    }

}

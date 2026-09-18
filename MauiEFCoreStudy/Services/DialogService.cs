using MauiEFCoreStudy.Services.Interfaces;

namespace MauiEFCoreStudy.Services;

/// <summary>
/// ダイアログメッセージサービス用クラス。
/// </summary>
public class DialogService : IDialogService
{

    public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
    {
        var application = Application.Current;
        if (application == null)
        {
            throw new InvalidOperationException("Application.Current is null");
        }

        var page = application.Windows[0].Page;
        if (page == null)
        {
            throw new InvalidOperationException("Current page is null");
        }

        return page.DisplayAlertAsync(title, message, accept, cancel);
    }

}

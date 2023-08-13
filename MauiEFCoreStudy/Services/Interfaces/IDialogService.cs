namespace MauiEFCoreStudy.Services.Interfaces;

/// <summary>
/// ダイアログメッセージサービス用インターフェース。
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// アラートダイアログを表示する。
    /// </summary>
    /// <param name="title">タイトル</param>
    /// <param name="message">メッセージ</param>
    /// <param name="accept"></param>
    /// <param name="cancel"></param>
    /// <returns></returns>
    Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
}

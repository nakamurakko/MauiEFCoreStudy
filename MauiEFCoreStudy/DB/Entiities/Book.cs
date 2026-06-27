using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MauiEFCoreStudy.DB.Entities;

/// <summary>
/// 本クラス。
/// </summary>
[Comment("書籍")]
public sealed class Book
{

    /// <summary>書籍 ID</summary>
    [Comment("書籍 ID")]
    public long BookId { get; set; }

    /// <summary>書籍名</summary>
    [Required]
    [MaxLength(100)]
    [Comment("書籍名")]
    public string Title { get; set; }

    /// <summary>著者 ID</summary>
    [Comment("著者 ID")]
    public long? AuthorId { get; set; }

    public Author? Author { get; set; }

}

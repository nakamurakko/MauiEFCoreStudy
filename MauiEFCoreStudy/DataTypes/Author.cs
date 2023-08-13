using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MauiEFCoreStudy.DataTypes;

/// <summary>
/// 著者クラス。
/// </summary>
[Table(nameof(Author))]
public class Author
{
    /// <summary>
    /// 著者ID。
    /// </summary>
    public long AuthorId { get; set; }

    /// <summary>
    /// 著者名。
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string AuthorName { get; set; }
}

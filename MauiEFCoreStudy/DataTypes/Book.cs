using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.DataTypes;

/// <summary>
/// 本クラス。
/// </summary>
[Table(nameof(Book))]
public class Book
{
    /// <summary>
    /// 本ID。
    /// </summary>
    public long BookId { get; set; }

    /// <summary>
    /// タイトル。
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    /// <summary>
    /// 著者ID。
    /// </summary>
    public long? AuthorId { get; set; }

    /// <summary>
    /// 著者。
    /// </summary>
    [ForeignKey(nameof(AuthorId))]
    public Author Author { get; set; }
}

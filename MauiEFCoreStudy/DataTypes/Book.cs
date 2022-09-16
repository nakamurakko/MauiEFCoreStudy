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
    /// <remarks>
    /// https://docs.microsoft.com/ja-jp/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt#included-and-excluded-properties
    /// </remarks>
    [ForeignKey(nameof(AuthorId))]
    [NotMapped]
    public Author Author { get; set; }
}

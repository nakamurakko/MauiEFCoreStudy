using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.Models;

/// <summary>
/// 本DBアクセス用 Model。
/// </summary>
public class BookModel
{
    /// <summary>
    /// 著者の一覧を取得する。
    /// </summary>
    /// <returns>著者の一覧。</returns>
    public static IEnumerable<Author> GetAuthors()
    {
        var authors = new List<Author>();

        using (var dbContext = new BookDBContext())
        {
            authors = dbContext.Authors.ToList();
        }

        return authors;
    }

    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <returns>本情報の一覧。</returns>
    public static IEnumerable<Book> GetBooks()
    {
        var books = new List<Book>();

        using (var dbContext = new BookDBContext())
        {
            books = dbContext.Books
                .GroupJoin(
                    dbContext.Authors,
                    book => book.AuthorId,
                    author => author.AuthorId,
                    (book, authors) => new { book, authors }
                )
                .SelectMany(
                    bookAndAuthors => bookAndAuthors.authors.DefaultIfEmpty(),
                    (bookAndAuthors, author) =>
                    new Book()
                    {
                        BookId = bookAndAuthors.book.BookId,
                        Title = bookAndAuthors.book.Title,
                        AuthorId = bookAndAuthors.book.AuthorId,
                        Author = author
                    }
                )
                .ToList();
        }

        return books;
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    /// <param name="book">本情報。</param>
    public static void AddBook(Book book)
    {
        using (var dbContext = new BookDBContext())
        {
            dbContext.Books.Add(
                new Book()
                {
                    Title = book.Title,
                    AuthorId = book.AuthorId
                });

            dbContext.SaveChanges();
        }
    }
}

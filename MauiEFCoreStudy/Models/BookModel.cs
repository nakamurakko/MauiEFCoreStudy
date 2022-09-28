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
    public static async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        var authors = new List<Author>();

        using (var dbContext = new BookDBContext())
        {
            authors = await dbContext.Authors.ToListAsync();
        }

        return authors;
    }

    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <returns>本情報の一覧。</returns>
    public static async Task<IEnumerable<Book>> GetBooksAsync()
    {
        var books = new List<Book>();

        using (var dbContext = new BookDBContext())
        {
            books = await dbContext.Books
                .GroupJoin(
                    dbContext.Authors,
                    book => book.AuthorId,
                    author => author.AuthorId,
                    (book, author) => new { book, author }
                )
                .SelectMany(
                    bookAndAuthor => bookAndAuthor.author.DefaultIfEmpty(),
                    (bookAndAuthor, author) =>
                    new Book()
                    {
                        BookId = bookAndAuthor.book.BookId,
                        Title = bookAndAuthor.book.Title,
                        AuthorId = bookAndAuthor.book.AuthorId,
                        Author = author
                    }
                )
                .ToListAsync();
        }

        return books;
    }

    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <param name="title">本のタイトル。部分一致検索する。</param>
    /// <returns>本情報の一覧。</returns>
    public static async Task<IEnumerable<Book>> GetBooksAsync(string title)
    {
        var books = new List<Book>();

        using (var dbContext = new BookDBContext())
        {
            books = await dbContext.Books
                .GroupJoin(
                    dbContext.Authors,
                    book => book.AuthorId,
                    author => author.AuthorId,
                    (book, author) => new { book, author }
                )
                .SelectMany(
                    bookAndAuthor => bookAndAuthor.author.DefaultIfEmpty(),
                    (bookAndAuthor, author) =>
                    new Book()
                    {
                        BookId = bookAndAuthor.book.BookId,
                        Title = bookAndAuthor.book.Title,
                        AuthorId = bookAndAuthor.book.AuthorId,
                        Author = author
                    }
                )
                .Where(book => book.Title.Contains(title))
                .ToListAsync();
        }

        return books;
    }

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    /// <param name="author"></param>
    public static void AddAuthor(Author author)
    {
        using (var dbContext = new BookDBContext())
        {
            dbContext.Authors.Add(author);

            dbContext.SaveChanges();
        }
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    /// <param name="book">本情報。</param>
    public static void AddBook(Book book)
    {
        using (var dbContext = new BookDBContext())
        {
            dbContext.Books.Add(book);

            dbContext.SaveChanges();
        }
    }
}

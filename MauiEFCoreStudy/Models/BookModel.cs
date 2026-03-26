using MauiEFCoreStudy.DataTypes;
using MauiEFCoreStudy.DB;
using Microsoft.EntityFrameworkCore;

namespace MauiEFCoreStudy.Models;

/// <summary>
/// 本DBアクセス用 Model。
/// </summary>
public sealed class BookModel
{
    /// <summary>
    /// 著者の一覧を取得する。
    /// </summary>
    /// <returns>著者の一覧。</returns>
    public static async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        List<Author> authors = new();

        using (BookDBContext dbContext = new())
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
        List<Book> books = new();

        using (BookDBContext dbContext = new())
        {
            //books = await dbContext.Books
            //    .GroupJoin(
            //        dbContext.Authors,
            //        book => book.AuthorId,
            //        author => author.AuthorId,
            //        (book, author) => new { book, author }
            //    )
            //    .SelectMany(
            //        bookAndAuthor => bookAndAuthor.author.DefaultIfEmpty(),
            //        (bookAndAuthor, author) =>
            //        new Book()
            //        {
            //            BookId = bookAndAuthor.book.BookId,
            //            Title = bookAndAuthor.book.Title,
            //            AuthorId = bookAndAuthor.book.AuthorId,
            //            Author = author
            //        }
            //    )
            //    .ToListAsync();

            // .NET 10 以降では LeftJoin を使う。(LinqKit に LeftJoin が存在するため注意する。)
            books = await dbContext.Books
                .LeftJoin(
                    dbContext.Authors,
                    book => book.AuthorId,
                    author => author.AuthorId,
                    (book, author) =>
                    new Book()
                    {
                        BookId = book.BookId,
                        Title = book.Title,
                        AuthorId = book.AuthorId,
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
        List<Book> books = new();

        using (BookDBContext dbContext = new())
        {
            //books = await dbContext.Books
            //    .GroupJoin(
            //        dbContext.Authors,
            //        book => book.AuthorId,
            //        author => author.AuthorId,
            //        (book, author) => new { book, author }
            //    )
            //    .SelectMany(
            //        bookAndAuthor => bookAndAuthor.author.DefaultIfEmpty(),
            //        (bookAndAuthor, author) =>
            //        new Book()
            //        {
            //            BookId = bookAndAuthor.book.BookId,
            //            Title = bookAndAuthor.book.Title,
            //            AuthorId = bookAndAuthor.book.AuthorId,
            //            Author = author
            //        }
            //    )
            //    .Where(book => book.Title.Contains(title))
            //    .ToListAsync();

            // .NET 10 以降では LeftJoin を使う。(LinqKit に LeftJoin が存在するため注意する。)
            books = await dbContext.Books
                .LeftJoin(
                    dbContext.Authors,
                    book => book.AuthorId,
                    author => author.AuthorId,
                    (book, author) =>
                    new Book()
                    {
                        BookId = book.BookId,
                        Title = book.Title,
                        AuthorId = book.AuthorId,
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
        using (BookDBContext dbContext = new())
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
        using (BookDBContext dbContext = new())
        {
            dbContext.Books.Add(book);

            dbContext.SaveChanges();
        }
    }
}

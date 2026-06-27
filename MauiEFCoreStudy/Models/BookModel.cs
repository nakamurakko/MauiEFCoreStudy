using MauiEFCoreStudy.DB;
using MauiEFCoreStudy.DB.Entities;
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
        using BookDbContext dbContext = new();

        return await dbContext.Authors.ToListAsync();
    }

    /// <summary>
    /// 本情報を取得する。
    /// </summary>
    /// <param name="title">本のタイトル。部分一致検索する。</param>
    /// <returns>本情報の一覧。</returns>
    public static async Task<IEnumerable<Book>> GetBooksAsync(string title = "")
    {
        using BookDbContext dbContext = new();

        LinqKit.ExpressionStarter<Book> predicateBuilder = LinqKit.PredicateBuilder.New<Book>(true);
        if (!string.IsNullOrWhiteSpace(title))
        {
            predicateBuilder.Or(x => x.Title.Contains(title));
        }

        //return await dbContext.Books
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

        //// .NET 10 以降では LeftJoin を使う。(LinqKit に LeftJoin が存在するため注意する。)
        //return await dbContext.Books
        //    .LeftJoin(
        //        dbContext.Authors,
        //        book => book.AuthorId,
        //        author => author.AuthorId,
        //        (book, author) =>
        //        new Book()
        //        {
        //            BookId = book.BookId,
        //            Title = book.Title,
        //            AuthorId = book.AuthorId,
        //            Author = author
        //        }
        //    )
        //    .Where(predicateBuilder)
        //    .ToListAsync();

        // QueryableExtensions.Include を使う。
        return await dbContext.Books
            .Include(book => book.Author)
            .Where(predicateBuilder)
            .ToListAsync();
    }

    /// <summary>
    /// 著者を追加する。
    /// </summary>
    /// <param name="author"></param>
    public static async Task AddAuthorAsync(Author author)
    {
        using BookDbContext dbContext = new();

        using (await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                await dbContext.Authors.AddAsync(author);
                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await dbContext.Database.RollbackTransactionAsync();

                throw;
            }
        }
    }

    /// <summary>
    /// 本を追加する。
    /// </summary>
    /// <param name="book">本情報。</param>
    public static async Task AddBookAsync(Book book)
    {
        using BookDbContext dbContext = new();

        using (await dbContext.Database.BeginTransactionAsync())
        {
            try
            {
                //await dbContext.Books.AddAsync(book);
                await dbContext.Books.AddAsync(new Book { Title = book.Title, AuthorId = book.AuthorId });
                await dbContext.SaveChangesAsync();
                await dbContext.Database.CommitTransactionAsync();

            }
            catch (Exception)
            {
                await dbContext.Database.RollbackTransactionAsync();

                throw;
            }
        }
    }

}

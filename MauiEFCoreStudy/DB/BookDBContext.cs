using MauiEFCoreStudy.DataTypes;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MauiEFCoreStudy.DB;

public sealed class BookDBContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // https://docs.microsoft.com/ja-jp/ef/core/dbcontext-configuration/
        // https://learn.microsoft.com/ja-jp/ef/core/what-is-new/ef-core-7.0/breaking-changes#encrypt-defaults-to-true-for-sql-server-connections
        optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=BookDB;User ID=BookUser;Password=bkusr;Persist Security Info=True;TrustServerCertificate=True");

        // ログをコンソールに出力する。
        // https://learn.microsoft.com/ja-jp/ef/core/logging-events-diagnostics/simple-logging#logging-to-the-console
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }
}

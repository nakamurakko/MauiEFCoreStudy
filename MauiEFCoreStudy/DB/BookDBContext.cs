using MauiEFCoreStudy.DataTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiEFCoreStudy.DB;

public class BookDBContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // https://docs.microsoft.com/ja-jp/ef/core/dbcontext-configuration/
        optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=BookDB;User ID=BookUser;Password=bkusr;Persist Security Info=True");

        // ログをコンソールに出力する。
        // https://learn.microsoft.com/ja-jp/ef/core/logging-events-diagnostics/simple-logging#logging-to-the-console
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }
}

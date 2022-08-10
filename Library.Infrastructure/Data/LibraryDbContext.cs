using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasMany(b => b.Ratings).WithOne(r => r.Book).HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Book>().HasMany(b => b.Reviews).WithOne(r => r.Book).HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using BookManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasIndex(book => book.Title)
            .IsUnique();
    }
}

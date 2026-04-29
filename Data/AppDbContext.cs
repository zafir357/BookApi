using BookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<Publisher> Publishers => Set<Publisher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Existing many-to-many config
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);

        // One-to-many: Publisher → Books
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.SetNull);

        // Seed publishers
        modelBuilder.Entity<Publisher>().HasData(
            new Publisher { Id = 1, Name = "Secker & Warburg", Country = "UK" },
            new Publisher { Id = 2, Name = "Chatto & Windus", Country = "UK" }
        );

        // Existing seed data — add PublisherId
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "George Orwell", Country = "UK" },
            new Author { Id = 2, Name = "Aldous Huxley", Country = "UK" }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "1984", Year = 1949, PublisherId = 1 },
            new Book { Id = 2, Title = "Brave New World", Year = 1932, PublisherId = 2 }
        );
        modelBuilder.Entity<BookAuthor>().HasData(
            new BookAuthor { BookId = 1, AuthorId = 1 },
            new BookAuthor { BookId = 2, AuthorId = 2 }
        );
    }
}
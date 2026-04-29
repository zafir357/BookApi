namespace BookApi.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Year { get; set; }

    // Foreign key to Publisher (nullable = book can exist without publisher)
    public int? PublisherId { get; set; }

    // Navigation property
    public Publisher? Publisher { get; set; }

    // Many-to-many navigation
    public ICollection<BookAuthor> BookAuthors { get; set; } = [];
}
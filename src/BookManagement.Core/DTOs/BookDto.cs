using BookManagement.Data.Entities;

namespace BookManagement.Core.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int PublicationYear { get; set; }
    public required string AuthorName { get; set; }
    public int ViewsCount { get; set; }
    public int YearsSincePublished { get; set; }
    public decimal PopularityScore { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static BookDto FromEntity(Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublicationYear = book.PublicationYear,
            AuthorName = book.AuthorName,
            ViewsCount = book.ViewsCount,
            YearsSincePublished = Book.GetAge(book.PublicationYear),
            PopularityScore = Book.GetPopularityScore(book.ViewsCount, book.PublicationYear),
            IsDeleted = book.IsDeleted,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}

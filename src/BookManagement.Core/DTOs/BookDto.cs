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
        var bookAge = DateTime.UtcNow.Year - book.PublicationYear;

        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            PublicationYear = book.PublicationYear,
            AuthorName = book.AuthorName,
            ViewsCount = book.ViewsCount,
            YearsSincePublished = bookAge,
            PopularityScore = book.ViewsCount * 0.5m + bookAge * 2,
            IsDeleted = book.IsDeleted,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}

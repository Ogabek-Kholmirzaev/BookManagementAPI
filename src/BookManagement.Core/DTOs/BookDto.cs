using BookManagement.Data.Entities;

namespace BookManagement.Core.DTOs;

/// <summary>
/// Data transfer object for book entity
/// </summary>
public class BookDto
{
    /// <summary>
    /// Book id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Book title
    /// </summary>
    public required string Title { get; set; }
    public int PublicationYear { get; set; }
    public required string AuthorName { get; set; }
    public int ViewsCount { get; set; }
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
            PopularityScore = Book.GetPopularityScore(book.ViewsCount, book.PublicationYear),
            IsDeleted = book.IsDeleted,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt
        };
    }
}

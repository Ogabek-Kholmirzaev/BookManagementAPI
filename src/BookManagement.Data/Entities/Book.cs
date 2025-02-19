namespace BookManagement.Data.Entities;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int PublicationYear { get; set; }
    public required string AuthorName { get; set; }
    public int ViewsCount { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public static int GetAge(int publicationYear)
    {
        return DateTime.UtcNow.Year - publicationYear;
    }

    public static decimal GetPopularityScore(int viewsCount, int publicationYear)
    {
        return viewsCount * 0.5m + GetAge(publicationYear) * 2;
    }
}

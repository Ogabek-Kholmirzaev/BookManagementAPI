using System.ComponentModel.DataAnnotations;
using BookManagement.Data.Entities;

namespace BookManagement.Core.DTOs;

public class CreateBookDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required, Range(1, 2025)]
    public int PublicationYear { get; set; }

    [Required]
    public string AuthorName { get; set; } = string.Empty;

    public static Book ToEntity(CreateBookDto dto)
    {
        return new Book
        {
            Title = dto.Title,
            PublicationYear = dto.PublicationYear,
            AuthorName = dto.AuthorName
        };
    }
}

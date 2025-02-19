using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.DTOs;

public class UpdateBookDto
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required, Range(1, 2025)]
    public int PublicationYear { get; set; }

    [Required]
    public string AuthorName { get; set; } = string.Empty;
}

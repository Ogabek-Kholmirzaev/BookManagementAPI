namespace BookManagement.Core.DTOs;

public class BookListDto(int Id, string Title)
{
    public int Id { get; } = Id;
    public string Title { get; } = Title;
}

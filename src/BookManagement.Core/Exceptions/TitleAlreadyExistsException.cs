namespace BookManagement.Core.Exceptions;

public class TitleAlreadyExistsException(string title)
    : Exception($"Title '{title}' already exists.")
{
}

namespace BookManagement.Core.Exceptions;

public class BookNotFoundException(int id) : Exception($"Book with id {id} was not found.")
{
}

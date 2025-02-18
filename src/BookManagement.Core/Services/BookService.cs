using BookManagement.Core.DTOs;
using BookManagement.Core.Exceptions;
using BookManagement.Core.Interfaces;
using BookManagement.Data.Repositories;

namespace BookManagement.Core.Services;

public class BookService(IBookRepository repository) : IBookService
{
    public async Task<int> AddAsync(CreateBookDto dto)
    {
        if (await repository.AnyAsync(book => !book.IsDeleted && book.Title == dto.Title))
        {
            throw new TitleAlreadyExistsException(dto.Title);
        }

        var book = CreateBookDto.ToEntity(dto);
        await repository.AddAsync(book);
        return book.Id;
    }
}

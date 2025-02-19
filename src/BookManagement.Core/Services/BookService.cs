using BookManagement.Core.DTOs;
using BookManagement.Core.Exceptions;
using BookManagement.Core.Interfaces;
using BookManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Core.Services;

public class BookService(IBookRepository repository) : IBookService
{
    public async Task<BookDto> GetByIdAsync(int id)
    {
        var book = await repository.GetByIdAsync(id) ?? throw new BookNotFoundException(id);

        book.ViewsCount++;
        await repository.UpdateAsync(book);

        return BookDto.FromEntity(book);
    }

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

    public async Task AddRangeAsync(IEnumerable<CreateBookDto> dtos)
    {
        var titles = await repository.GetAll()
            .Where(book => !book.IsDeleted)
            .Select(book => book.Title)
            .ToListAsync();

        var exsistingTitles = new HashSet<string>();

        foreach (var dto in dtos)
        {
            if (titles.Contains(dto.Title))
            {
                exsistingTitles.Add(dto.Title);
            }
        }

        if (exsistingTitles.Count != 0)
        {
            throw new TitleAlreadyExistsException(string.Join(',', exsistingTitles));
        }

        var books = dtos.Select(CreateBookDto.ToEntity);
        await repository.AddRangeAsync(books);
    }

    public async Task UpdateAsync(int id, UpdateBookDto dto)
    {
        var book = await repository.GetByIdAsync(id) ?? throw new BookNotFoundException(id);

        if (await repository.AnyAsync(book => !book.IsDeleted && book.Title == dto.Title))
        {
            throw new TitleAlreadyExistsException(dto.Title);
        }

        book.Title = dto.Title;
        book.PublicationYear = dto.PublicationYear;
        book.AuthorName = dto.AuthorName;

        await repository.UpdateAsync(book);
    }

    public async Task DeleteAsync(int id)
    {
        var book = await repository.GetByIdAsync(id) ?? throw new BookNotFoundException(id);
        await repository.DeleteAsync(book);
    }

    public async Task<(bool, IEnumerable<int>)> DeleteRangeAsync(HashSet<int> ids)
    {
        var books = await repository.GetAll()
            .Where(book => !book.IsDeleted && ids.Contains(book.Id))
            .ToListAsync();

        if (books.Count == ids.Count)
        {
            await repository.DeleteRangeAsync(books);
            return (true, []);
        }

        IEnumerable<int> notFoundIds = ids.Except(books.Select(book => book.Id));
        return (false, notFoundIds);
    }
}

using System.Linq.Expressions;
using BookManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Data.Repositories;

public class BookRepository(AppDbContext context) : IBookRepository
{
    public IQueryable<Book> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await context.Books.FirstOrDefaultAsync(book => !book.IsDeleted && book.Id == id);
    }

    public async Task<Book> AddAsync(Book book)
    {
        var entry = await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task UpdateAsync(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        book.IsDeleted = true;
        await context.SaveChangesAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<Book, bool>> predicate)
    {
        return context.Books.AnyAsync(predicate);
    }


    public async Task AddRangeAsync(IEnumerable<Book> books)
    {
        await context.Books.AddRangeAsync(books);
        await context.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(List<Book> books)
    {
        books.ForEach(book => book.IsDeleted = true);
        await context.SaveChangesAsync();
    }
}

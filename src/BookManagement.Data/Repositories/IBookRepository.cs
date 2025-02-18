using System.Linq.Expressions;
using BookManagement.Data.Entities;

namespace BookManagement.Data.Repositories;

public interface IBookRepository
{
    IQueryable<Book> GetAll();
    Task<Book?> GetByIdAsync(int id);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
    Task<bool> AnyAsync(Expression<Func<Book, bool>> predicate);
    Task AddRangeAsync(IEnumerable<Book> books);
    Task DeleteRangeAsync(List<Book> books);
}

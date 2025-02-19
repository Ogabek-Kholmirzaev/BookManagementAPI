using BookManagement.Core.DTOs;

namespace BookManagement.Core.Interfaces;

public interface IBookService
{
    Task<BookDto> GetByIdAsync(int id);
    Task<int> AddAsync(CreateBookDto dto);
    Task AddRangeAsync(IEnumerable<CreateBookDto> dtos);
    Task UpdateAsync(int id, UpdateBookDto dto);
    Task DeleteAsync(int id);
    Task<(bool, IEnumerable<int>)> DeleteRangeAsync(HashSet<int> ids);
}

using BookManagement.Core.DTOs;
using BookManagement.Core.Pagination;

namespace BookManagement.Core.Interfaces;

public interface IBookService
{
    Task<PagedResult<BookListDto>> GetAllAsync(PaginationParams @params);
    Task<BookDto> GetByIdAsync(int id);
    Task<BookDto> AddAsync(CreateBookDto dto);
    Task<IEnumerable<int>> AddRangeAsync(IEnumerable<CreateBookDto> dtos);
    Task UpdateAsync(int id, UpdateBookDto dto);
    Task DeleteAsync(int id);
    Task<(bool, IEnumerable<int>)> DeleteRangeAsync(HashSet<int> ids);
}

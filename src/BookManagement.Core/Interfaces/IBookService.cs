using BookManagement.Core.DTOs;
using BookManagement.Data.Entities;

namespace BookManagement.Core.Interfaces;

public interface IBookService
{
    Task<int> AddAsync(CreateBookDto dto);
}

using BookManagement.Core.DTOs;
using BookManagement.Core.Interfaces;
using BookManagement.Core.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService service) : ControllerBase
{
    /// <summary>
    /// Get all books with pagination
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     Get api/books?pageNumber=1&amp;pageSize=10
    /// </remarks>
    /// <param name="params">Pagination parameters</param>
    /// <returns>Paged result of books</returns>
    /// <returns code="200">Returns the paged result of books</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<BookListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var pagedBooks = await service.GetAllAsync(@params);
        return Ok(pagedBooks);
    }

    /// <summary>
    /// Get a book by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     Get api/books/1
    /// </remarks>
    /// <param name="id">Book id</param>
    /// <returns>A book</returns>
    /// <returns code="200">Returns the book</returns>
    /// <returns code="404">If the book is not found</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var bookDto = await service.GetByIdAsync(id);
        return Ok(bookDto);
    }

    /// <summary>
    /// Create a new book
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST api/books
    ///     {
    ///         "title": "Algorithms and Data Structures",
    ///         "publicationYear": 2022,
    ///         "authorName": "John Doe"
    ///     }
    /// </remarks>
    /// <param name="dto">Book data</param>
    /// <returns>A newly created book</returns>
    /// <returns code="201">Returns the newly created book</returns>
    /// <returns code="400">If the request is invalid</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] CreateBookDto dto)
    {
        var bookDto = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { bookDto.Id }, bookDto);
    }

    /// <summary>
    /// Create multiple books
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST api/books/bulk
    ///     [
    ///         {
    ///             "title": "Algorithms and Data Structures",
    ///             "publicationYear": 2022,
    ///             "authorName": "John Doe"
    ///         },
    ///         {
    ///             "title": "Design Patterns",
    ///             "publicationYear": 2021,
    ///             "authorName": "Jane Doe"
    ///         }
    ///     ]
    /// </remarks>
    /// <param name="dtos">Book datas</param>
    /// <returns>Created books</returns>
    /// <returns code="201">Returns the created books</returns>
    /// <returns code="400">If the request is invalid</returns>
    [HttpPost("bulk")]
    [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> AddBulk([FromBody] IEnumerable<CreateBookDto> dtos)
    {
        var bookDtos = await service.AddRangeAsync(dtos);
        return CreatedAtAction(nameof(GetAll), bookDtos);
    }

    /// <summary>
    /// Update a book
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT api/books/1
    ///     {
    ///         "title": "Algorithms and Data Structures",
    ///         "publicationYear": 2022,
    ///         "authorName": "John Doe"
    ///     }
    /// </remarks>
    /// <param name="id">Book id</param>
    /// <param name="dto">Book data</param>
    /// <returns>No content</returns>
    /// <returns code="204">If the book is updated successfully</returns>
    /// <returns code="400">If the request is invalid</returns>
    /// <returns code="404">If the book is not found</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Delete a book
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE api/books/1
    ///     {
    ///         "title": "Algorithms and Data Structures",
    ///         "publicationYear": 2022,
    ///         "authorName": "John Doe"
    ///     }
    /// </remarks>
    /// <param name="id">Book id</param>
    /// <returns>No content</returns>
    /// <returns code="204">If the book is deleted successfully</returns>
    /// <returns code="404">If the book is not found</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Delete multiple books
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     DELETE api/books/bulk
    ///     [1, 2, 3]
    /// </remarks>
    /// <param name="ids">Book ids</param>
    /// <returns>No content</returns>
    /// <returns code="204">If the books are deleted successfully</returns>
    /// <returns code="400">If the request is invalid</returns>
    /// <returns code="404">If the books are not found</returns>
    [HttpDelete("bulk")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBulk([FromBody] HashSet<int> ids)
    {
        var (isSuccess, notFoundIds) = await service.DeleteRangeAsync(ids);
        return isSuccess ? NoContent() : NotFound(notFoundIds);
    }
}

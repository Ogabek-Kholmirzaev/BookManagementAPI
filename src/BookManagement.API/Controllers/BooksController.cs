using BookManagement.Core.DTOs;
using BookManagement.Core.Interfaces;
using BookManagement.Core.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<BookListDto>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var pagedBooks = await service.GetAllAsync(@params);
        return Ok(pagedBooks);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var bookDto = await service.GetByIdAsync(id);
        return Ok(bookDto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] CreateBookDto dto)
    {
        var bookDto = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { bookDto.Id }, bookDto);
    }

    [HttpPost("bulk")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> AddBulk([FromBody] IEnumerable<CreateBookDto> dtos)
    {
        var ids = await service.AddRangeAsync(dtos);
        return Ok(ids);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("bulk")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(IEnumerable<int>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBulk([FromBody] HashSet<int> ids)
    {
        var (isSuccess, notFoundIds) = await service.DeleteRangeAsync(ids);
        return isSuccess ? NoContent() : NotFound(notFoundIds);
    }
}

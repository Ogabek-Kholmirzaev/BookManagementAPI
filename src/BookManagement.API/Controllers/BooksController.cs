using BookManagement.Core.DTOs;
using BookManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(IBookService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBookDto dto)
    {
        var id = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> AddBulk([FromBody] IEnumerable<CreateBookDto> dtos)
    {
        await service.AddRangeAsync(dtos);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("bulk")]
    public async Task<IActionResult> DeleteBulk([FromBody] HashSet<int> ids)
    {
        var (isSuccess, notFoundIds) = await service.DeleteRangeAsync(ids);
        return isSuccess ? NoContent() : NotFound(notFoundIds);
    }
}

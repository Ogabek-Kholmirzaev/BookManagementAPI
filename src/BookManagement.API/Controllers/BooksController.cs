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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> AddBulk([FromBody] IEnumerable<CreateBookDto> dtos)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await service.AddRangeAsync(dtos);
        return Ok();
    }
}

using Microsoft.AspNetCore.Mvc;
using BookTradeHubAPI.Services;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.DTO.Create;

namespace BookTradeHubAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet]
    public async Task<ActionResult<List<BookGetDto>>> GetAllAsync()
    {
        return Ok(await _bookService.GetAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookGetDto>> GetByIdAsync(string id)
    {
        try
        {
            return await _bookService.GetAsync(id);
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(BookCreateDto newBook)
    {
        try
        {
            await _bookService.CreateAsync(newBook);

            return Created();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(string id, BookCreateDto updatedBook)
    {
        try
        {
            await _bookService.UpdateAsync(id, updatedBook);
            return Ok(await _bookService.GetAsync(id));
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        try
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }
}

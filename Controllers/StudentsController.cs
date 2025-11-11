using Microsoft.AspNetCore.Mvc;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Services;
using BookTradeHubAPI.Models.DTO.Create;

namespace BookTradeHubAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentGetDto>>> GetAllAsync()
    {
        return Ok(await _studentService.GetAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentGetDto>> GetByIdAsync(string id)
    {
        try
        {
            return Ok(await _studentService.GetAsync(id));
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(StudentCreateDto newStudent)
    {
        await _studentService.CreateAsync(newStudent);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StudentGetDto>> UpdateAsync(string id, StudentCreateDto updatedStudent)
    {
        try
        {
            await _studentService.UpdateAsync(id, updatedStudent);
            return (await _studentService.GetAsync(id));
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(string id)
    {
        try
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }
}

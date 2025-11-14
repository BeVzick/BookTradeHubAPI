using Microsoft.AspNetCore.Mvc;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Services;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookTradeHubAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult> CreateAsync(StudentCreateDto newStudent)
    {
        try
        {
            await _studentService.CreateAsync(newStudent);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        return Created();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> Login(LoginModel model)
    {
        try
        {
            return Ok(await _studentService.Login(model));
        }
        catch (InvalidOperationException)
        {
            return Unauthorized("Email or password is wrong");
        }
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

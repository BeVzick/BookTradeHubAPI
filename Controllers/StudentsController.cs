using Microsoft.AspNetCore.Mvc;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Services;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models;
using Microsoft.AspNetCore.Authorization;
using BookTradeHubAPI.Enums;
using System.Security.Claims;

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

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginModel model)
    {
        try
        {
            return Ok(await _studentService.LoginAsync(model));
        }
        catch (InvalidOperationException)
        {
            return Unauthorized("Email or password is wrong");
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> CreateAsync(StudentCreateDto newStudent)
    {
        try
        {
            await _studentService.CreateAsync(newStudent);
            return Created();
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("refresh")]
    [Authorize]
    public async Task<ActionResult<LoginResponse>> Refresh([FromHeader(Name = "Authorization")] string authHeader, [FromBody] string refreshToken)
    {
        try
        {
            return Ok(await _studentService.RefreshAsync(authHeader, refreshToken));
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
        catch (AccessViolationException e)
        {
            return Unauthorized(e.Message);
        }
    }

    [HttpGet("whoami")]
    [Authorize]
    public ActionResult WhoAmI()
    {
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        return Ok(new { roles });
    }

    [HttpPut("addRole")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<ActionResult> AddRoleAsync(string id, Roles role)
    {
        try
        {
            await _studentService.AddRoleAsync(id, role);
            return NoContent();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpPut("removeRole")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<ActionResult> RemoveRoleAsync(string id, Roles role)
    {
        try
        {
            await _studentService.RemoveRoleAsync(id, role);
            return NoContent();
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<StudentGetDto>>> GetAllAsync()
    {
        return Ok(await _studentService.GetAsync());
    }

    [HttpGet("{id}")]
    [Authorize]
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

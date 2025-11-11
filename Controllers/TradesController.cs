using Microsoft.AspNetCore.Mvc;
using BookTradeHubAPI.Services;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.DTO.Create;

namespace BookTradeHubAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradesController : ControllerBase
{
    private readonly ITradeService _tradeService;

    public TradesController(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TradeGetDto>>> GetAllAsync()
    {
        return Ok(await _tradeService.GetAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TradeGetDto>> GetByIdAsync(string id)
    {
        try
        {
            return Ok(await _tradeService.GetAsync(id));
        }
        catch (NullReferenceException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAsync(TradeCreateDto newTrade)
    {
        try
        {
            await _tradeService.CreateAsync(newTrade);
            return Created();
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
}

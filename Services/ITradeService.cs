using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;

namespace BookTradeHubAPI.Services;

public interface ITradeService
{
    Task CreateAsync(TradeCreateDto trade);
    Task<List<TradeGetDto>> GetAsync();
    Task<TradeGetDto> GetAsync(string id);
}

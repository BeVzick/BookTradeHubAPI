using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Repositories;

public interface ITradeRepository
{
    Task CreateAsync(Trade trade);
    Task <List<Trade>> GetAllAsync();
    Task<Trade?> GetByIdAsync(string id);
}

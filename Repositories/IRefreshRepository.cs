using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Repositories;

public interface IRefreshRepository
{
    Task CreateAsync(Refresh refresh);
    Task<Refresh?> GetByStudentIdAsync(string id);
    Task UpdateAsync(string id, Refresh refresh);
}

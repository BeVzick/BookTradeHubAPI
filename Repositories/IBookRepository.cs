using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Repostories;

public interface IBookRepository
{
    Task CreateAsync(Book book);
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(string id);
    Task UpdateAsync(Book book);
    Task DeleteAsync(string id);
}

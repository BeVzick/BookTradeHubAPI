using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Repositories;

public interface IBookRepository
{
    Task CreateAsync(Book book);
    Task<List<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(string id);
    Task<List<Book>> GetByOwnerIdAsync(string ownerId);
    Task UpdateAsync(string id, Book book);
    Task DeleteAsync(string id);
}

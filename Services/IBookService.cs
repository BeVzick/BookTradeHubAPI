using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;

namespace BookTradeHubAPI.Services;

public interface IBookService
{
    Task CreateAsync(BookCreateDto book);
    Task<List<BookGetDto>> GetAsync();
    Task<BookGetDto> GetAsync(string id);
    Task<List<BookGetDto>> GetByOwnerAsync(string owner);
    Task UpdateAsync(string id, BookCreateDto book);
    Task DeleteAsync(string id);
}

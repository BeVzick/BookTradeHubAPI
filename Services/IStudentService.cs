using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;

namespace BookTradeHubAPI.Services;

public interface IStudentService
{
    Task CreateAsync(StudentCreateDto student);
    Task<List<StudentGetDto>> GetAsync();
    Task<StudentGetDto> GetAsync(string id);
    Task UpdateAsync(string id, StudentCreateDto student);
    Task DeleteAsync(string id);
}

using BookTradeHubAPI.Models;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;

namespace BookTradeHubAPI.Services;

public interface IStudentService
{
    Task CreateAsync(StudentCreateDto student);
    Task<List<StudentGetDto>> GetAsync();
    Task<StudentGetDto> GetAsync(string id);
    Task<StudentGetDto> GetByEmailAsync(string email);
    Task<LoginResponse> Login(LoginModel model);
    Task UpdateAsync(string id, StudentCreateDto student);
    Task DeleteAsync(string id);
}

using BookTradeHubAPI.Enums;
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
    Task<LoginResponse> LoginAsync(LoginModel model);
    Task<LoginResponse> RefreshAsync(string authHeader, string refreshToken);
    Task AddRoleAsync(string id, Roles role);
    Task RemoveRoleAsync(string id, Roles role);
    Task UpdateAsync(string id, StudentCreateDto student);
    Task DeleteAsync(string id);
}

using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Repositories;

public interface IStudentRepository
{
    Task CreateAsync(Student student);
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(string id);
    Task UpdateAsync(Student student);
    Task DeleteAsync(string id);
}

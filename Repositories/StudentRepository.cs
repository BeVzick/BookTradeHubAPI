using BookTradeHubAPI.Models.Entity;
using MongoDB.Driver;

namespace BookTradeHubAPI.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly IMongoCollection<Student> _collection;

    public StudentRepository(MongoDBClient client) => 
        _collection = client.GetCollection<Student>("students");

    public async Task CreateAsync(Student student) =>
        await _collection.InsertOneAsync(student);

    public async Task<List<Student>> GetAllAsync() =>
        await (await _collection.FindAsync(_ => true)).ToListAsync();

    public async Task<Student?> GetByIdAsync(string id) =>
        await (await _collection.FindAsync(s => s.Id == id)).FirstOrDefaultAsync();

    public async Task UpdateAsync(string id, Student student) =>
        await _collection.ReplaceOneAsync(s => s.Id == id, student);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(s => s.Id == id);
}

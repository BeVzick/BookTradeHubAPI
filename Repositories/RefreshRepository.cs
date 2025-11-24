using BookTradeHubAPI.Models.Entity;
using MongoDB.Driver;

namespace BookTradeHubAPI.Repositories;

public class RefreshRepository : IRefreshRepository
{
    private readonly IMongoCollection<Refresh> _collection;

    public RefreshRepository(MongoDBClient client) =>
        _collection = client.GetCollection<Refresh>("refreshes");

    public async Task CreateAsync(Refresh refresh) =>
        await _collection.InsertOneAsync(refresh);

    public async Task<Refresh?> GetByStudentIdAsync(string id) =>
        await (await _collection.FindAsync(r => r.StudentId == id)).FirstOrDefaultAsync();

    public async Task UpdateAsync(string id, Refresh refresh) =>
        await _collection.ReplaceOneAsync(r => r.Id == id, refresh);
}

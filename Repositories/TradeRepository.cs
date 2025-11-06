using BookTradeHubAPI.Models.Entity;
using MongoDB.Driver;

namespace BookTradeHubAPI.Repositories;

public class TradeRepository : ITradeRepository
{
    private readonly IMongoCollection<Trade> _collection;

    public TradeRepository() =>
        _collection = MongoDBClient.Instance.GetCollection<Trade>("trades");

    public async Task CreateAsync(Trade trade) =>
        await _collection.InsertOneAsync(trade);

    public async Task<List<Trade>> GetAllAsync() =>
        await (await _collection.FindAsync(_ => true)).ToListAsync();

    public async Task<Trade?> GetByIdAsync(string id) =>
        await (await _collection.FindAsync(t => t.Id == id)).FirstOrDefaultAsync();    
}

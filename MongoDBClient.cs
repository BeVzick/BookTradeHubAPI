using BookTradeHubAPI.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BookTradeHubAPI;

public class MongoDBClient
{
    private IMongoDatabase _db;

    public MongoDBClient(IOptions<MongoDBSettings> options)
    {
        var settings = options.Value;
        var connectionString = settings.ConnectionString;
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name) => _db.GetCollection<T>(name);
}

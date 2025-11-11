using MongoDB.Driver;

namespace BookTradeHubAPI;

public class MongoDBClient
{
    private static IMongoDatabase _db;
    private static MongoDBClient _instanse;

    public static MongoDBClient Instance
    {
        get => _instanse ?? new MongoDBClient();
    }

    private MongoDBClient()
    {
        var connectionString = "mongodb://localhost:27017";
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase("BookTradeHubDB");
    }

    public IMongoCollection<T> GetCollection<T>(string name) => _db.GetCollection<T>(name);
}

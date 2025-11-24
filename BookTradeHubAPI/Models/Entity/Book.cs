using BookTradeHubAPI.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookTradeHubAPI.Models.Entity;

public class Book
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; }

    [BsonElement("author")]
    public string Author { get; set; }

    [BsonElement("genre")]
    [BsonRepresentation(BsonType.Int64)]
    public Genre Genre { get; set; }

    [BsonElement("ownerId")]
    public string OwnerId { get; set; }
}

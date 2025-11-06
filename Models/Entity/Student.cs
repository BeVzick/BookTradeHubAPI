using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookTradeHubAPI.Models.Entity;

public class Student
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("firstName")]
    public string FirstName { get; set; } = default!;

    [BsonElement("lastName")]
    public string LastName { get; set; } = default!;

    [BsonElement("age")]
    public int Age { get; set; } = default!;

    [BsonElement("bookIds")]
    public List<string> BookIds { get; set; } = default!;
}

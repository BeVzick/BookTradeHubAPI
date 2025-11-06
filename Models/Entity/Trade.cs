using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookTradeHubAPI.Models.Entity;

public class Trade
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    [BsonElement("date")]
    public DateTime Date { get; set; }

    [BsonElement("student1Id")]
    public string Student1Id { get; set; } = default!;

    [BsonElement("student2Id")]
    public string Student2Id { get; set; } = default!;

    [BsonElement("newStudent1BookIds")]
    public List<string> newStudent1BookIds { get; set; } = default!;

    [BsonElement("newStudent2BookIds")]
    public List<string> newStudent2BookIds { get; set; } = default!;
}

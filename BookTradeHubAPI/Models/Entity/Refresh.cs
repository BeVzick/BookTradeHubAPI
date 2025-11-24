using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BookTradeHubAPI.Models.Entity;

public class Refresh
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("studentId")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string StudentId { get; set; }

    [BsonElement("refreshToken")]
    public string? RefreshToken { get; set; }

    [BsonElement("refreshTokenExpiryTime")]
    public DateTime? RefreshTokenExpiryTime { get; set; }
}

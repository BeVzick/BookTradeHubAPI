using MongoDB.Bson.Serialization.Attributes;

namespace BookTradeHubAPI.Models.DTO.Create;

public class StudentCreateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

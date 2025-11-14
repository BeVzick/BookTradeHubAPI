using MongoDB.Bson.Serialization.Attributes;

namespace BookTradeHubAPI.Models.DTO.Get;

public class StudentGetDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public List<BookGetDto> Books { get; set; }
}

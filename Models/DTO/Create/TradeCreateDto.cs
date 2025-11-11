namespace BookTradeHubAPI.Models.DTO.Create;

public class TradeCreateDto
{
    public string Student1Id { get; set; }
    public string Student2Id { get; set; }
    public List<string> newStudent1BookIds { get; set; }
    public List<string> newStudent2BookIds { get; set; }
}

namespace BookTradeHubAPI.Models.DTO.Get;

public class TradeGetDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public StudentGetDto Student1 { get; set; }
    public StudentGetDto Student2 { get; set; }
    public List<BookGetDto> newStudent1Books { get; set; }
    public List<BookGetDto> newStudent2Books { get; set; }
}

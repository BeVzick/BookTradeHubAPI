namespace BookTradeHubAPI.Models;

public class Trade
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Student1Id { get; set; }
    public int Student2Id { get; set; }
    public List<int> newStudent1BookIds { get; set; }
    public List<int> newStudent2BookIds { get; set; }
}

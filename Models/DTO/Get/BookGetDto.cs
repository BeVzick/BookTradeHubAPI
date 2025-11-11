using BookTradeHubAPI.Enums;

namespace BookTradeHubAPI.Models.DTO.Get;

public class BookGetDto
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public Genre Genre { get; set; }
}

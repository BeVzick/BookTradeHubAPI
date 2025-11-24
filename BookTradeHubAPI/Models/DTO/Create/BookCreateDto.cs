using BookTradeHubAPI.Enums;

namespace BookTradeHubAPI.Models.DTO.Create;

public class BookCreateDto
{
    public string Title { get; set; }
    public string Author { get; set; }
    public Genre Genre { get; set; }
    public string OwnerId { get; set; }
}

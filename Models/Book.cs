using BookTradeHubAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace BookTradeHubAPI.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Field 'Title' is required")]
    [MaxLength(100, ErrorMessage = "Field 'Title' cannot be longer than 100 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Field 'Author' is required")]
    [MaxLength(100, ErrorMessage = "Field 'Author' cannot be longer than 100 characters")]
    [RegularExpression(@"^[A-Z]{1}[a-z]+\s[A-Z]{1}[a-z]+$", ErrorMessage = "Field 'Author' is not valid")]
    public string Author { get; set; }

    [Required(ErrorMessage = "Field 'Genre' is required")]
    [EnumDataType(typeof(Genre), ErrorMessage = "Field 'Genre' is not valid")]
    public Genre Genre { get; set; }

    [Required(ErrorMessage = "Filed 'OwnerId' is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Filed 'OwnerId' cannot be negative")]
    public int OwnerId { get; set; }
}

using BookTradeHubAPI.Models;

namespace BookTradeHubAPI.Data;

public static class BookData
{
    public static List<Book> Books = new List<Book>()
    {
        new Book { Id = 0, Title = "Pro ASP.NET Core 8 MVC", Author = "Adam Freeman", OwnerId = 1 },
        new Book { Id = 1, Title = "ASP.NET Core in Action", Author = "Andrew Lock", OwnerId = 2 },
        new Book { Id = 2, Title = "Professional ASP.NET Core 8", Author = "Christian Wenz", OwnerId = 0 },
        new Book { Id = 3, Title = "Blazor WebAssembly by Exmple", Author = "Carl Rippon", OwnerId = 0 },
        new Book { Id = 4, Title = "Architecting Modern Web Application with ASP.NET Core and Azure", Author = "Steve Smith", OwnerId = 1 }
    };
}

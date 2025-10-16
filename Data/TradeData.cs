using BookTradeHubAPI.Models;

namespace BookTradeHubAPI.Data;

public static class TradeData
{
    public static List<Trade> Trades = new List<Trade>()
    {
        new Trade { 
            Id = 0, 
            Date = new DateTime(new DateOnly(2024, 9, 21), new TimeOnly(13, 34)), 
            Student1Id = 0, Student2Id = 1, 
            newStudent1BookIds = new List<int>{ 2, 3 }, 
            newStudent2BookIds = new List<int>{ 0 }
        },
        new Trade {
            Id = 1,
            Date = new DateTime(new DateOnly(), new TimeOnly()),
            Student1Id = 1, Student2Id = 2,
            newStudent1BookIds = new List<int>{ 4 },
            newStudent2BookIds = new List<int>{ 1 }
        }
    };
}

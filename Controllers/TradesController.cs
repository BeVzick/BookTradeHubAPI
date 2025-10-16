using BookTradeHubAPI.Models;
using BookTradeHubAPI.Data;
using BookTradeHubAPI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BookTradeHubAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TradesController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Trade>> GetAll(string? field, int? value)
    {
        List<Trade> trades = TradeData.Trades;

        if (field != null && value != null)
        {
            TradeFields tradeField;
            if (Enum.TryParse(field, out tradeField))
            {
                trades = tradeField switch
                {
                    TradeFields.Student1Id => trades.FindAll(t => t.Student1Id == value),
                    TradeFields.Student2Id => trades.FindAll(t => t.Student2Id == value),
                };
            }
        }

        return Ok(trades);
    }

    [HttpGet("{id}")]
    public ActionResult<Trade> GetById(int id)
    {
        Trade? trade = TradeData.Trades.FirstOrDefault(t => t.Id == id);
        if (trade is null)
            return NotFound();

        return Ok(trade);
    }

    [HttpPost]
    public ActionResult<Trade> Create(Trade newTrade)
    {
        newTrade.Id = TradeData.Trades.Max(t => t.Id) + 1;
        newTrade.Date = DateTime.Now;

        Student student1 = StudentData.Students.FirstOrDefault(s => s.Id == newTrade.Student1Id);
        Student student2 = StudentData.Students.FirstOrDefault(s => s.Id == newTrade.Student2Id);

        newTrade.newStudent1BookIds.ForEach(item => {
            Book book = BookData.Books.FirstOrDefault(b => b.Id == item);
            book.OwnerId = student1.Id;
            student2.BookIds.Remove(item);
            student1.BookIds.Add(item);
        });
        newTrade.newStudent2BookIds.ForEach(item => {
            Book book = BookData.Books.FirstOrDefault(b => b.Id == item);
            book.OwnerId = student2.Id;
            student1.BookIds.Remove(item);
            student2.BookIds.Add(item);
        });

        TradeData.Trades.Add(newTrade);
        return CreatedAtAction(nameof(GetById), new { id = newTrade.Id }, newTrade);
    }
}

using AutoMapper;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using BookTradeHubAPI.Repositories;

namespace BookTradeHubAPI.Services;

public class TradeService : ITradeService
{
    private readonly ITradeRepository _tradeRepo;
    private readonly IStudentService _studentService;
    private readonly IBookService _bookService;
    private readonly IMapper _mapper;

    public TradeService(ITradeRepository tradeRepo, IStudentService studentService, IBookService bookService, IMapper mapper)
    {
        _tradeRepo = tradeRepo;
        _studentService = studentService;
        _bookService = bookService;
        _mapper = mapper;
    }

    public async Task CreateAsync(TradeCreateDto trade)
    {
        if (await _studentService.GetAsync(trade.Student1Id) == null)
            throw new ArgumentException($"Student with id:{trade.Student1Id} doesn't exists");
        
        if (await _studentService.GetAsync(trade.Student2Id) == null)
            throw new ArgumentException($"Student with id:{trade.Student2Id} doesn't exists");

        List<BookGetDto> newStudent2Books = await _bookService.GetByOwnerAsync(trade.Student1Id);
        if (!newStudent2Books.All(b => trade.newStudent2BookIds.Contains(b.Id)))
            throw new ArgumentException($"Student with id:{trade.Student1Id} doesn't have expected books");

        List<BookGetDto> newStudent1Books = (await _bookService.GetByOwnerAsync(trade.Student2Id));
        if (!newStudent1Books.All(b => trade.newStudent1BookIds.Contains(b.Id)))
            throw new ArgumentException($"Student with id:{trade.Student2Id} doesn't have expected books");

        
        for (int i = 0; i < newStudent1Books.Count; i++)
        {
            BookCreateDto book = new BookCreateDto();
            book.Title = newStudent1Books[i].Title;
            book.Author = newStudent1Books[i].Author;
            book.Genre = newStudent1Books[i].Genre;
            book.OwnerId = trade.Student1Id;

            await _bookService.UpdateAsync(newStudent1Books[i].Id, book);
        }
        for (int i = 0; i < newStudent2Books.Count; i++)
        {
            BookCreateDto book = new BookCreateDto();
            book.Title = newStudent2Books[i].Title;
            book.Author = newStudent2Books[i].Author;
            book.Genre = newStudent2Books[i].Genre;
            book.OwnerId = trade.Student2Id;

            await _bookService.UpdateAsync(newStudent2Books[i].Id, book);
        }

        await _tradeRepo.CreateAsync(_mapper.Map<Trade>(trade));
    }

    public async Task<List<TradeGetDto>> GetAsync()
    {
        List<Trade> trades = await _tradeRepo.GetAllAsync();
        List<TradeGetDto> getTrades = _mapper.Map<List<TradeGetDto>>(trades);

        for (int i = 0; i < trades.Count; i++)
        {
            getTrades[i].Student1 = await _studentService.GetAsync(trades[i].Student1Id);
            getTrades[i].Student2 = await _studentService.GetAsync(trades[i].Student2Id);

            for (int j = 0; j < trades[i].newStudent1BookIds.Count; j++)
                getTrades[i].newStudent1Books.Add(await _bookService.GetAsync(trades[i].newStudent1BookIds[j]));
            for (int j = 0; j < trades[i].newStudent2BookIds.Count; j++)
                getTrades[i].newStudent2Books.Add(await _bookService.GetAsync(trades[i].newStudent2BookIds[j]));
        }

        return getTrades;
    }

    public async Task<TradeGetDto> GetAsync(string id)
    {
        Trade? trade = await _tradeRepo.GetByIdAsync(id);
        if (trade == null)
            throw new NullReferenceException($"Trade with id:{id} doesn't exist");

        TradeGetDto getTrade = _mapper.Map<TradeGetDto>(trade);
        getTrade.Student1 = await _studentService.GetAsync(trade.Student1Id);
        getTrade.Student2 = await _studentService.GetAsync(trade.Student2Id);

        foreach (var bookId in trade.newStudent1BookIds)
            getTrade.newStudent1Books.Add(await _bookService.GetAsync(bookId));
        foreach (var bookId in trade.newStudent2BookIds)
            getTrade.newStudent2Books.Add(await _bookService.GetAsync(bookId));

        return getTrade;
    }
}

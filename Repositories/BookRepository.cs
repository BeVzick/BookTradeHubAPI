using BookTradeHubAPI.Models.Entity;
using MongoDB.Driver;

namespace BookTradeHubAPI.Repositories;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _collection;

    public BookRepository() =>
        _collection = MongoDBClient.Instance.GetCollection<Book>("books");

    public async Task CreateAsync(Book book) =>
        await _collection.InsertOneAsync(book);

    public async Task<List<Book>> GetAllAsync() =>
        await (await _collection.FindAsync(_ => true)).ToListAsync();

    public async Task<Book?> GetByIdAsync(string id) =>
        await (await _collection.FindAsync(b => b.Id == id)).FirstOrDefaultAsync();

    public async Task<List<Book>> GetByOwnerIdAsync(string ownerId) =>
        await (await _collection.FindAsync(b => b.OwnerId == ownerId)).ToListAsync();

    public async Task UpdateAsync(string id, Book book) =>
        await _collection.ReplaceOneAsync(b => b.Id == id, book);

    public async Task DeleteAsync(string id) => 
        await _collection.DeleteOneAsync(b => b.Id == id);
}

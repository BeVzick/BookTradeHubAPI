using AutoMapper;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using BookTradeHubAPI.Repositories;

namespace BookTradeHubAPI.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepo, IStudentService studentService, IMapper mapper)
        {
            _bookRepo = bookRepo;
            _studentService = studentService;
            _mapper = mapper;
        }

        public async Task CreateAsync(BookCreateDto book)
        {
            if (await _studentService.GetAsync(book.OwnerId) == null)
                throw new ArgumentException($"Student with id:{book.OwnerId} doesn't exists");

            await _bookRepo.CreateAsync(_mapper.Map<Book>(book));
        }

        public async Task<List<BookGetDto>> GetAsync() =>
            _mapper.Map<List<BookGetDto>>(await _bookRepo.GetAllAsync());

        public async Task<BookGetDto> GetAsync(string id)
        {
            Book? book = await _bookRepo.GetByIdAsync(id);
            if (book == null)
                throw new NullReferenceException($"Book with id:{id} doesn't exist");

            return _mapper.Map<BookGetDto>(book);
        }

        public async Task<List<BookGetDto>> GetByOwnerAsync(string id)
        {
            if (await _studentService.GetAsync(id) == null)
                throw new ArgumentException($"Student with id:{id} doesn't exists");

            return _mapper.Map<List<BookGetDto>>(await _bookRepo.GetByOwnerIdAsync(id));
        }

        public async Task UpdateAsync(string id, BookCreateDto book)
        {
            if (await _bookRepo.GetByIdAsync(id) == null)
                throw new NullReferenceException($"Book with id:{id} doesn't exist");
            if (await _studentService.GetAsync(book.OwnerId) == null)
                throw new ArgumentException($"Student with id:{book.OwnerId} doesn't exists");

            Book newBook = _mapper.Map<Book>(book);
            newBook.Id = id;
            await _bookRepo.UpdateAsync(id, newBook);
        }

        public async Task DeleteAsync(string id)
        {
            if (await _bookRepo.GetByIdAsync(id) == null)
                throw new NullReferenceException($"Book with id:{id} doesn't exist");

            await _bookRepo.DeleteAsync(id);
        }
    }
}

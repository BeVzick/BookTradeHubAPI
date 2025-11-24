using AutoMapper;
using BookTradeHubAPI.Enums;
using BookTradeHubAPI.Models.DTO.Get;
using BookTradeHubAPI.Models.Entity;
using BookTradeHubAPI.Repositories;
using BookTradeHubAPI.Services;
using Moq;

namespace UnitTestProject;

public class ResponceTests
{
    [Fact]
    public async void GetMethod1()
    {
        var students = new List<Student>
        {
            new Student { Id = "sid1", FirstName = "FirstName1", LastName = "LastName1", Age = 21,
                Email = "student1@gmail.com", Password = "password1", Role = Roles.Student },
            new Student { Id = "sid2", FirstName = "FirstName2", LastName = "LastName2", Age = 23,
                Email = "student2@gmail.com", Password = "password1", Role = Roles.Manager },
            new Student { Id = "sid3", FirstName = "FirstName3", LastName = "LastName3", Age = 18,
                Email = "student3@gmail.com", Password = "password1", Role = Roles.Admin }
        };

        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(() => students);

        var books = new List<Book>
        {
            new Book { Id = "bid1", Title = "Title1", Author = "Author1", Genre = Genre.Science, OwnerId = "sid1" },
            new Book { Id = "bid2", Title = "Title2", Author = "Author2", Genre = Genre.Fiction, OwnerId = "sid1" },
            new Book { Id = "bid3", Title = "Title3", Author = "Author3", Genre = Genre.Drama, OwnerId = "sid2" }
        };

        var _mockBookRepo = new Mock<IBookRepository>();
        foreach (var student in students)
            _mockBookRepo.Setup(r => r.GetByOwnerIdAsync(student.Id))
                .ReturnsAsync((string id) => books.Where(b => b.OwnerId == id).ToList());

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Student, StudentGetDto>();
            cfg.CreateMap<Book, BookGetDto>();
        });
        var service = new StudentService(_mockStudentRepo.Object, null!, _mockBookRepo.Object, null!, null!, config.CreateMapper());

        Assert.Collection(await service.GetAsync(),
            s =>
            {
                Assert.Equal("sid1", s.Id);
                Assert.Equal(2, s.Books.Count);
                Assert.Contains(s.Books, b => b.Id == "bid1");
                Assert.Contains(s.Books, b => b.Id == "bid2");
            },
            s =>
            {
                Assert.Equal("sid2", s.Id);
                Assert.Single(s.Books);
                Assert.Equal("bid3", s.Books[0].Id);
            },
            s =>
            {
                Assert.Equal("sid3", s.Id);
                Assert.Empty(s.Books);
            });
    }

    [Fact]
    public async void GetMethod2()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByIdAsync("sid1")).ReturnsAsync(() => new Student
        {
            Id = "sid1",
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 21,
            Email = "student@gmail.com",
            Password = "password"
        });

        var books = new List<Book>
        {
            new Book { Id = "bid1", Title = "Title1", Author = "Author1", Genre = Genre.Science, OwnerId = "sid1" },
            new Book { Id = "bid2", Title = "Title2", Author = "Author2", Genre = Genre.Fiction, OwnerId = "sid1" },
            new Book { Id = "bid3", Title = "Title3", Author = "Author3", Genre = Genre.Drama, OwnerId = "sid2" }
        };

        var _mockBookRepo = new Mock<IBookRepository>();
        _mockBookRepo.Setup(r => r.GetByOwnerIdAsync("sid1"))
                .ReturnsAsync((string id) => books.Where(b => b.OwnerId == id).ToList());

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Student, StudentGetDto>();
            cfg.CreateMap<Book, BookGetDto>();
        });
        var service = new StudentService(_mockStudentRepo.Object, null!, _mockBookRepo.Object, null!, null!, config.CreateMapper());

        var result = await service.GetAsync("sid1");

        var expected = new StudentGetDto
        {
            Id = "sid1",
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 21,
            Books = new List<BookGetDto>
            {
                new BookGetDto { Id = "bid1", Title = "Title1", Author = "Author1", Genre = Genre.Science },
                new BookGetDto { Id = "bid2", Title = "Title2", Author = "Author2", Genre = Genre.Fiction }
            }
        };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.FirstName, result.FirstName);
        Assert.Equal(expected.LastName, result.LastName);
        Assert.Equal(expected.Age, result.Age);
        Assert.Collection(result.Books,
            b => Assert.Equal("bid1", b.Id),
            b => Assert.Equal("bid2", b.Id));
    }

    [Fact]
    public async void GetMethod3()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByIdAsync("sid1")).ReturnsAsync(() => new Student
        {
            Id = "sid1",
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 21,
            Email = "student@gmail.com",
            Password = "password"
        });
        _mockStudentRepo.Setup(r => r.GetByEmailAsync("student@gmail.com")).ReturnsAsync(() => new Student
        {
            Id = "sid1",
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 21,
            Email = "student@gmail.com",
            Password = "password"
        });

        var books = new List<Book>
        {
            new Book { Id = "bid1", Title = "Title1", Author = "Author1", Genre = Genre.Science, OwnerId = "sid1" },
            new Book { Id = "bid2", Title = "Title2", Author = "Author2", Genre = Genre.Fiction, OwnerId = "sid1" },
            new Book { Id = "bid3", Title = "Title3", Author = "Author3", Genre = Genre.Drama, OwnerId = "sid2" }
        };

        var _mockBookRepo = new Mock<IBookRepository>();
        _mockBookRepo.Setup(r => r.GetByOwnerIdAsync("sid1"))
                .ReturnsAsync((string id) => books.Where(b => b.OwnerId == id).ToList());

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Student, StudentGetDto>();
            cfg.CreateMap<Book, BookGetDto>();
        });
        var service = new StudentService(_mockStudentRepo.Object, null!, _mockBookRepo.Object, null!, null!, config.CreateMapper());

        var result = await service.GetAsync("sid1");

        var expected = new StudentGetDto
        {
            Id = "sid1",
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 21,
            Books = new List<BookGetDto>
            {
                new BookGetDto { Id = "bid1", Title = "Title1", Author = "Author1", Genre = Genre.Science },
                new BookGetDto { Id = "bid2", Title = "Title2", Author = "Author2", Genre = Genre.Fiction }
            }
        };

        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.FirstName, result.FirstName);
        Assert.Equal(expected.LastName, result.LastName);
        Assert.Equal(expected.Age, result.Age);
        Assert.Collection(result.Books,
            b => Assert.Equal("bid1", b.Id),
            b => Assert.Equal("bid2", b.Id));
    }
}

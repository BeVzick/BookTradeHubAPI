using BookTradeHubAPI.Enums;
using BookTradeHubAPI.Models;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Models.Entity;
using BookTradeHubAPI.Repositories;
using BookTradeHubAPI.Services;
using Moq;

namespace UnitTestProject;

public class InvalidOperationTests
{
    [Fact]
    public async void CreateTestMethod()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByEmailAsync("student@gmail.com")).ReturnsAsync(() => new Student
        {
            Id = "sid1",
            FirstName = "StudentFirstName",
            LastName = "StudentLastName",
            Age = 21,
            Email = "student@gmail.com",
            Password = "password",
            Role = Roles.Student
        });

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        var student = new StudentCreateDto
        {
            FirstName = "StudentFirstName",
            LastName = "StudentLastName",
            Age = 21,
            Email = "student@gmail.com",
            Password = "password",
        };

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.CreateAsync(student));
    }

    [Fact]
    public async void LoginTestMethod1()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByEmailAsync("email@gamil.com")).ReturnsAsync(() => null!);

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        var login = new LoginModel
        {
            Email = "email@gamil.com",
            Password = "password"
        };

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.LoginAsync(login));
    }

    [Fact]
    public async void LoginTestMethod2()
    {
        var _mockPasswordHasher = new Mock<IPasswordHasher>();
        _mockPasswordHasher.Setup(ph => ph.VerifyPassword("errorPassword", "password")).Returns(false);

        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByEmailAsync("student@gmail.com")).ReturnsAsync(() => new Student
        {
            Id = "sid1",
            FirstName = "StudentFirstName",
            LastName = "StudentLastName",
            Age = 21,
            Email = "student@gmail.com",
            Password = "password",
            Role = Roles.Student
        });

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, _mockPasswordHasher.Object, null!);

        var login = new LoginModel
        {
            Email = "student@gamil.com",
            Password = "errorPassword"
        };

        await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.LoginAsync(login));
    }

    // ...
}

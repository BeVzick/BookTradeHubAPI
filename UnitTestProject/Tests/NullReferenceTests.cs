using BookTradeHubAPI.Enums;
using BookTradeHubAPI.Models.DTO.Create;
using BookTradeHubAPI.Repositories;
using BookTradeHubAPI.Services;
using Moq;

namespace UnitTestProject;

public class NullReferenceTests
{
    [Fact]
    public async void GetByIdTestMethod()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByIdAsync("sid1")).ReturnsAsync(() => null!);

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        await Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetAsync("sid1"));
    }

    [Fact]
    public async void GetByEmailTestMethod()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByEmailAsync("email@gamil.com")).ReturnsAsync(() => null!);

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        await Assert.ThrowsAsync<NullReferenceException>(async () => await service.GetByEmailAsync("email@gmail.com"));
    }

    [Fact]
    public async void UpdateTestMethod()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByIdAsync("sid2")).ReturnsAsync(() => null!);

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        StudentCreateDto student = new StudentCreateDto
        {
            FirstName = "StudentFistName",
            LastName = "StudentLastName",
            Age = 21,
            Email = "student@gaml.com",
            Password = "password"
        };

        await Assert.ThrowsAsync<NullReferenceException>(async () => await service.UpdateAsync("sid2", student));
    }

    [Fact]
    public async void DeleteTestMethod()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByIdAsync("sid3")).ReturnsAsync(() => null!);

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        await Assert.ThrowsAsync<NullReferenceException>(async () => await service.DeleteAsync("sid3"));
    }

    [Fact]
    public async void AddRoleTestMethod()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByIdAsync("sids4")).ReturnsAsync(() => null!);

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        await Assert.ThrowsAsync<NullReferenceException>(async () => await service.AddRoleAsync("sid4", Roles.Manager));
    }

    [Fact]
    public async void RemoveRoleTestMethod()
    {
        var _mockStudentRepo = new Mock<IStudentRepository>();
        _mockStudentRepo.Setup(r => r.GetByIdAsync("sids5")).ReturnsAsync(() => null!);

        var service = new StudentService(_mockStudentRepo.Object, null!, null!, null!, null!, null!);

        await Assert.ThrowsAsync<NullReferenceException>(async () => await service.RemoveRoleAsync("sid5", Roles.Manager));
    }
}

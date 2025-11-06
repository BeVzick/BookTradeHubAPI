using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Data;

public static class StudentData
{
    public static List<Student> Students { get; set; } = new List<Student>()
    {
        new Student { Id = 0, FirstName = "Vladyslav", LastName = "Bevz", Age = 17 , BookIds = new List<int>{ 2, 3 } },
        new Student { Id = 1, FirstName = "Nazar", LastName = "Sheikin", Age = 17 , BookIds = new List<int>{ 0, 4 } },
        new Student { Id = 2, FirstName = "John", LastName = "Doe", Age = 21 , BookIds = new List<int>{ 1 } }
    };
}

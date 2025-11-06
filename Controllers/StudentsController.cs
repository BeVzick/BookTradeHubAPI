using BookTradeHubAPI.Data;
using BookTradeHubAPI.Enums;
using Microsoft.AspNetCore.Mvc;
using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Student>> GetAll(string? field, string? value)
    {
        List<Student> students = StudentData.Students;

        if (field != null && value != null)
        {
            StudentFields studentField;
            if (Enum.TryParse(field, out studentField))
            {
                students = studentField switch
                {
                    StudentFields.FirstName => students.FindAll(s => s.FirstName == value),
                    StudentFields.LastName => students.FindAll(s => s.LastName == value),
                    StudentFields.Age => students.FindAll(s => s.Age == Convert.ToInt32(value))
                };
            }
        }

        return Ok(students);
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        Student? student = StudentData.Students.FirstOrDefault(s => s.Id == id);
        if (student is null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create(Student newStudent)
    {
        newStudent.Id = StudentData.Students.Max(s => s.Id) + 1;
        StudentData.Students.Add(newStudent);
        return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
    }

    [HttpPut("{id}")]
    public ActionResult<Student> Update(int id, Student updatedStudent)
    {
        Student? student = StudentData.Students.FirstOrDefault(s => s.Id == id);
        if (student is null)
            return NotFound();

        student.FirstName = updatedStudent.FirstName;
        student.LastName = updatedStudent.LastName;
        student.Age = updatedStudent.Age;

        return Ok(student);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        Student? student = StudentData.Students.FirstOrDefault(s => s.Id == id);
        if (student is null)
            return NotFound();

        foreach (int bookId in student.BookIds)
            BookData.Books.RemoveAll(b => b.Id == bookId);

        StudentData.Students.Remove(student);
        return NoContent();
    }
}

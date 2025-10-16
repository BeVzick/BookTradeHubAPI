using BookTradeHubAPI.Models;
using BookTradeHubAPI.Data;
using BookTradeHubAPI.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BookTradeHubAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{

    [HttpGet]
    public ActionResult<List<Book>> GetAll(string? field, string? value)
    {
        List<Book> books = BookData.Books;

        if (field != null && value != null)
        {
            BookFields bookField;
            if (Enum.TryParse(field, out bookField))
            {
                books = bookField switch
                {
                    BookFields.Title => books.FindAll(b => b.Title == value),
                    BookFields.Author => books.FindAll(b => b.Author == value),
                    BookFields.OwnerId => books.FindAll(b => b.OwnerId == Convert.ToInt32(value))
                };
            }
        }

        return Ok(books);
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id)
    {
        Book? book = BookData.Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound();

        return Ok(book);
    }

    [HttpPost]
    public ActionResult<Book> Create(Book newBook)
    {
        newBook.Id = BookData.Books.Max(b => b.Id) + 1;
        BookData.Books.Add(newBook);
        StudentData.Students.FirstOrDefault(s => s.Id == newBook.OwnerId).BookIds.Add(newBook.Id);

        return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
    }

    [HttpPut("{id}")]
    public ActionResult<Book> Update(int id, Book updatedBook)
    {
        Book? book = BookData.Books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound();

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;
        if (book.OwnerId != updatedBook.OwnerId)
        {
            Student previousOwner = StudentData.Students.FirstOrDefault(s => s.Id == book.OwnerId);
            previousOwner.BookIds.Remove(book.Id);

            Student newOwner = StudentData.Students.FirstOrDefault(s => s.Id == updatedBook.OwnerId);
            newOwner.BookIds.Add(book.Id);

            book.OwnerId = updatedBook.OwnerId;
        }

        return Ok(book);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        Book? book = BookData.Books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound();

        Student? owner = StudentData.Students.FirstOrDefault(s => s.Id == book.OwnerId);
        owner.BookIds.Remove(book.Id);
        BookData.Books.Remove(book);

        return NoContent();
    }
}

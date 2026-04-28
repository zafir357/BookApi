using AutoMapper;
using BookApi.Data;
using BookApi.DTOs;
using BookApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(AppDbContext db, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await db.Books
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .ToListAsync();

        return Ok(mapper.Map<IEnumerable<BookDto>>(books));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var book = await db.Books
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null) return NotFound();
        return Ok(mapper.Map<BookDto>(book));
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> Create(CreateBookDto dto)
    {
        var book = mapper.Map<Book>(dto);

        foreach (var authorId in dto.AuthorIds)
            book.BookAuthors.Add(new BookAuthor { AuthorId = authorId });

        db.Books.Add(book);
        await db.SaveChangesAsync();

        // Reload with relations
        await db.Entry(book).Collection(b => b.BookAuthors)
            .Query().Include(ba => ba.Author).LoadAsync();

        return CreatedAtAction(nameof(GetById),
            new { id = book.Id }, mapper.Map<BookDto>(book));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateBookDto dto)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null) return NotFound();

        mapper.Map(dto, book);
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null) return NotFound();

        db.Books.Remove(book);
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("by-author/{authorId}")]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetByAuthor(int authorId)
    {
        var books = await db.Books
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorId))
            .ToListAsync();

        return Ok(mapper.Map<IEnumerable<BookDto>>(books));
    }
}
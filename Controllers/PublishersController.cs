using AutoMapper;
using BookApi.Data;
using BookApi.DTOs;
using BookApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PublishersController(AppDbContext db, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PublisherDto>>> GetAll()
    {
        var publishers = await db.Publishers
            .Include(p => p.Books)
            .ToListAsync();

        return Ok(mapper.Map<IEnumerable<PublisherDto>>(publishers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PublisherDto>> GetById(int id)
    {
        var publisher = await db.Publishers
            .Include(p => p.Books)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (publisher is null) return NotFound();
        return Ok(mapper.Map<PublisherDto>(publisher));
    }

    [HttpPost]
    public async Task<ActionResult<PublisherDto>> Create(CreatePublisherDto dto)
    {
        var publisher = mapper.Map<Publisher>(dto);
        db.Publishers.Add(publisher);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById),
            new { id = publisher.Id }, mapper.Map<PublisherDto>(publisher));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdatePublisherDto dto)
    {
        var publisher = await db.Publishers.FindAsync(id);
        if (publisher is null) return NotFound();

        mapper.Map(dto, publisher);
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var publisher = await db.Publishers.FindAsync(id);
        if (publisher is null) return NotFound();
        db.Publishers.Remove(publisher);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
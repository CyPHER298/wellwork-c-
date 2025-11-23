using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LembreteController : ControllerBase
{
    private readonly AppDbContext _context;

    public LembreteController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Lembrete>> GetAll()
        => await _context.Lembretes.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Lembrete>> Get(int id)
    {
        var item = await _context.Lembretes.FindAsync(id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Lembrete lemb)
    {
        _context.Lembretes.Add(lemb);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = lemb.IdLembrete }, lemb);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Lembrete lemb)
    {
        if (id != lemb.IdLembrete) return BadRequest();

        _context.Entry(lemb).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var lemb = await _context.Lembretes.FindAsync(id);
        if (lemb == null) return NotFound();

        _context.Lembretes.Remove(lemb);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
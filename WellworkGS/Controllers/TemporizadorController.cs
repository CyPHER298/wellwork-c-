using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemporizadorController : ControllerBase
{
    private readonly AppDbContext _context;

    public TemporizadorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Temporizador>> GetAll()
        => await _context.Temporizadores.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Temporizador>> Get(int id)
    {
        var timer = await _context.Temporizadores.FindAsync(id);
        return timer == null ? NotFound() : timer;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Temporizador temporizador)
    {
        _context.Temporizadores.Add(temporizador);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = temporizador.IdTemporizador }, temporizador);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Temporizador temporizador)
    {
        if (id != temporizador.IdTemporizador) return BadRequest();

        _context.Entry(temporizador).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var temporizador = await _context.Temporizadores.FindAsync(id);
        if (temporizador == null) return NotFound();

        _context.Temporizadores.Remove(temporizador);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
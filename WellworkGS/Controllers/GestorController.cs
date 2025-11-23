using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GestorController : ControllerBase
{
    private readonly AppDbContext _context;

    public GestorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Gestor>> GetAll()
        => await _context.Gestores.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Gestor>> Get(int id)
    {
        var gestor = await _context.Gestores.FindAsync(id);
        return gestor == null ? NotFound() : gestor;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Gestor gestor)
    {
        _context.Gestores.Add(gestor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = gestor.IdGestor }, gestor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Gestor gestor)
    {
        if (id != gestor.IdGestor) return BadRequest();

        _context.Entry(gestor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var gestor = await _context.Gestores.FindAsync(id);
        if (gestor == null) return NotFound();

        _context.Remove(gestor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
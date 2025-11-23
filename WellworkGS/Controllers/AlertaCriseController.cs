using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertaCriseController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlertaCriseController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<AlertaCrise>> GetAll()
        => await _context.AlertasCrise
            .Include(a => a.Usuario)
            .Include(a => a.Gestor)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<AlertaCrise>> Get(int id)
    {
        var item = await _context.AlertasCrise
            .Include(a => a.Usuario)
            .Include(a => a.Gestor)
            .FirstOrDefaultAsync(a => a.IdAlertaCrise == id);

        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult> Create(AlertaCrise alerta)
    {
        _context.AlertasCrise.Add(alerta);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = alerta.IdAlertaCrise }, alerta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AlertaCrise alerta)
    {
        if (id != alerta.IdAlertaCrise) return BadRequest();

        _context.Entry(alerta).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.AlertasCrise.FindAsync(id);
        if (item == null) return NotFound();

        _context.AlertasCrise.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetaController : ControllerBase
{
    private readonly AppDbContext _context;

    public MetaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Meta>> GetAll()
        => await _context.Metas.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Meta>> Get(int id)
    {
        var meta = await _context.Metas.FindAsync(id);
        return meta == null ? NotFound() : meta;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Meta meta)
    {
        _context.Metas.Add(meta);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = meta.IdMeta }, meta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Meta meta)
    {
        if (id != meta.IdMeta) return BadRequest();

        _context.Entry(meta).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var meta = await _context.Metas.FindAsync(id);
        if (meta == null) return NotFound();

        _context.Metas.Remove(meta);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
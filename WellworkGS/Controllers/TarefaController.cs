using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly AppDbContext _context;

    public TarefaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Tarefa>> GetAll()
        => await _context.Tarefas.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Tarefa>> Get(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        return tarefa == null ? NotFound() : tarefa;
    }

    [HttpPost]
    public async Task<ActionResult> Create(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = tarefa.IdTarefa }, tarefa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Tarefa tarefa)
    {
        if (id != tarefa.IdTarefa)
            return BadRequest();

        _context.Entry(tarefa).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) return NotFound();

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
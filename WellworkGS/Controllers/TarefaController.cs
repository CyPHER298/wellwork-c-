using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Service;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly TarefaService _service;

    public TarefaController(TarefaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarefaReadDTO>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TarefaReadDTO>> GetById(int id)
    {
        var tarefa = await _service.GetByIdAsync(id);
        return tarefa == null ? NotFound() : Ok(tarefa);
    }

    [HttpPost]
    public async Task<ActionResult<TarefaReadDTO>> Create([FromBody] TarefaCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdTarefa }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TarefaUpdateDTO dto)
    {
        var success = await _service.UpdateAsync(id, dto);
        return success ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Service;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemporizadorController : ControllerBase
{
    private readonly TemporizadorService _service;

    public TemporizadorController(TemporizadorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemporizadorReadDTO>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TemporizadorReadDTO>> GetById(int id)
    {
        var temporizador = await _service.GetByIdAsync(id);
        return temporizador == null ? NotFound() : Ok(temporizador);
    }

    [HttpPost]
    public async Task<ActionResult<TemporizadorReadDTO>> Create([FromBody] TemporizadorCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdTemporizador }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TemporizadorUpdateDTO dto)
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
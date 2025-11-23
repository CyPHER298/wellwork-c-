using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Service;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GestorController : ControllerBase
{
    private readonly GestorService _service;

    public GestorController(GestorService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GestorReadDTO>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GestorReadDTO>> GetById(int id)
    {
        var gestor = await _service.GetByIdAsync(id);
        return gestor == null ? NotFound() : Ok(gestor);
    }

    [HttpPost]
    public async Task<ActionResult<GestorReadDTO>> Create([FromBody] GestorCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdGestor }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] GestorUpdateDTO dto)
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
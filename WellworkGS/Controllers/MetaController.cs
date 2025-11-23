using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Service;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetaController : ControllerBase
{
    private readonly MetaService _service;

    public MetaController(MetaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MetaReadDTO>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MetaReadDTO>> GetById(int id)
    {
        var meta = await _service.GetByIdAsync(id);
        return meta == null ? NotFound() : Ok(meta);
    }

    [HttpPost]
    public async Task<ActionResult<MetaReadDTO>> Create([FromBody] MetaCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdMeta }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MetaUpdateDTO dto)
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
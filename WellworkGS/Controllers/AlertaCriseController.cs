using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Service;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertaCriseController : ControllerBase
{
    private readonly AlertaCriseService _service;

    public AlertaCriseController(AlertaCriseService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlertaCriseReadDTO>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AlertaCriseReadDTO>> GetById(int id)
    {
        var alerta = await _service.GetByIdAsync(id);
        return alerta == null ? NotFound() : Ok(alerta);
    }

    [HttpPost]
    public async Task<ActionResult<AlertaCriseReadDTO>> Create([FromBody] AlertaCriseCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdAlertaCrise }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AlertaCriseUpdateDTO dto)
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
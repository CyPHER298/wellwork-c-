using Microsoft.AspNetCore.Mvc;
using WellworkGS.DTOs;
using WellworkGS.Service;

namespace WellworkGS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _service;

    public UsuarioController(UsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioReadDTO>> GetById(int id)
    {
        var user = await _service.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioReadDTO>> Create([FromBody] UsuarioCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.IdUsuario }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UsuarioUpdateDTO dto)
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
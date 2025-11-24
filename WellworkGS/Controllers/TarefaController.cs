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
    
    [HttpGet("search")]
        public async Task<ActionResult<PagedResultDTO<TarefaResourceDTO>>> Search(
            [FromQuery] string? titulo,
            [FromQuery] string? status,
            [FromQuery] DateTime? dataInicio,
            [FromQuery] DateTime? dataFim,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "dataHoraTarefa",
            [FromQuery] string sortDirection = "asc"
        )
        {
            var tarefas = await _service.GetAllAsync();

            // filtros
            if (!string.IsNullOrWhiteSpace(titulo))
                tarefas = tarefas.Where(t => t.TituloTarefa != null && t.TituloTarefa.Contains(titulo, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(status))
                tarefas = tarefas.Where(t => t.StatusTarefa != null && t.StatusTarefa.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (dataInicio.HasValue)
                tarefas = tarefas.Where(t => t.DataHoraTarefa.HasValue && t.DataHoraTarefa.Value >= dataInicio.Value);

            if (dataFim.HasValue)
                tarefas = tarefas.Where(t => t.DataHoraTarefa.HasValue && t.DataHoraTarefa.Value <= dataFim.Value);

            // ordenação
            tarefas = (sortBy.ToLower(), sortDirection.ToLower()) switch
            {
                ("titulo", "desc") => tarefas.OrderByDescending(t => t.TituloTarefa),
                ("titulo", _)    => tarefas.OrderBy(t => t.TituloTarefa),

                ("status", "desc") => tarefas.OrderByDescending(t => t.StatusTarefa),
                ("status", _)       => tarefas.OrderBy(t => t.StatusTarefa),

                ("dataHoratarefa", "desc") => tarefas.OrderByDescending(t => t.DataHoraTarefa),
                _                           => tarefas.OrderBy(t => t.DataHoraTarefa)
            };

            // paginação
            var totalCount = tarefas.Count();
            var items = tarefas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            var controllerName = "Tarefa";

            // para cada item, gerar HATEOAS links
            var itemsWithLinks = items.Select(t =>
            {
                var links = new List<LinkDTO>
                {
                    new LinkDTO { Href = $"{baseUrl}/api/{controllerName}/{t.IdTarefa}", Rel = "self", Method = "GET" },
                    new LinkDTO { Href = $"{baseUrl}/api/{controllerName}/{t.IdTarefa}", Rel = "update_tarefa", Method = "PUT" },
                    new LinkDTO { Href = $"{baseUrl}/api/{controllerName}/{t.IdTarefa}", Rel = "delete_tarefa", Method = "DELETE" }
                };

                return new TarefaResourceDTO
                {
                    Data = t,
                    Links = links
                };
            }).ToList();

            // construir resultado paginado
            var result = new PagedResultDTO<TarefaResourceDTO>
            {
                Items = itemsWithLinks,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            // links de paginação
            result.Links.Add(new LinkDTO { Href = Url.Action(nameof(Search), controllerName, new { titulo, status, dataInicio, dataFim, sortBy, sortDirection, pageNumber, pageSize }, Request.Scheme)!, Rel = "self", Method = "GET" });

            if (pageNumber > 1)
            {
                result.Links.Add(new LinkDTO { Href = Url.Action(nameof(Search), controllerName, new { titulo, status, dataInicio, dataFim, sortBy, sortDirection, pageNumber = pageNumber - 1, pageSize }, Request.Scheme)!, Rel = "prev_page", Method = "GET" });
            }
            if (pageNumber * pageSize < totalCount)
            {
                result.Links.Add(new LinkDTO { Href = Url.Action(nameof(Search), controllerName, new { titulo, status, dataInicio, dataFim, sortBy, sortDirection, pageNumber = pageNumber + 1, pageSize }, Request.Scheme)!, Rel = "next_page", Method = "GET" });
            }

            return Ok(result);
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
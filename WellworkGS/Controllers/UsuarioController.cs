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
    
    [HttpGet("search")]
    public async Task<ActionResult<PagedResultDTO<UsuarioResourceDTO>>> Search(
        [FromQuery] string? nome,
        [FromQuery] string? email,
        [FromQuery] string? cargo,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "nome",          // nome | email | cargo
        [FromQuery] string sortDirection = "asc"     // asc | desc
    )
    {
        // 1) Busca todos via service
        var usuarios = await _service.GetAllAsync();

        // 2) Filtros
        if (!string.IsNullOrWhiteSpace(nome))
            usuarios = usuarios.Where(u =>
                u.NomeUsuario.Contains(nome, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(email))
            usuarios = usuarios.Where(u =>
                u.EmailUsuario.Contains(email, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(cargo))
            usuarios = usuarios.Where(u =>
                u.CargoUsuario.Contains(cargo, StringComparison.OrdinalIgnoreCase));

        // 3) Ordenação
        usuarios = (sortBy.ToLower(), sortDirection.ToLower()) switch
        {
            ("email", "desc") => usuarios.OrderByDescending(u => u.EmailUsuario),
            ("email", _)      => usuarios.OrderBy(u => u.EmailUsuario),

            ("cargo", "desc") => usuarios.OrderByDescending(u => u.CargoUsuario),
            ("cargo", _)      => usuarios.OrderBy(u => u.CargoUsuario),

            // default: nome
            ("nome", "desc")  => usuarios.OrderByDescending(u => u.NomeUsuario),
            _                 => usuarios.OrderBy(u => u.NomeUsuario)
        };

        // 4) Paginação
        var totalCount = usuarios.Count();
        var itemsPage = usuarios
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        var controllerName = "Usuario";

        // 5) HATEOAS por item
        var itemsComLinks = itemsPage.Select(u =>
        {
            var links = new List<LinkDTO>
            {
                new LinkDTO
                {
                    Href = $"{baseUrl}/api/{controllerName}/{u.IdUsuario}",
                    Rel = "self",
                    Method = "GET"
                },
                new LinkDTO
                {
                    Href = $"{baseUrl}/api/{controllerName}/{u.IdUsuario}",
                    Rel = "update_usuario",
                    Method = "PUT"
                },
                new LinkDTO
                {
                    Href = $"{baseUrl}/api/{controllerName}/{u.IdUsuario}",
                    Rel = "delete_usuario",
                    Method = "DELETE"
                }
            };

            return new UsuarioResourceDTO
            {
                Data = u,
                Links = links
            };
        }).ToList();

        // 6) Paginação + HATEOAS do recurso de página
        var result = new PagedResultDTO<UsuarioResourceDTO>
        {
            Items = itemsComLinks,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };

        // Links de paginação (HATEOAS collection-level)
        var queryBase = new
        {
            nome,
            email,
            cargo,
            sortBy,
            sortDirection
        };

        // self
        result.Links.Add(new LinkDTO
        {
            Href = Url.Action(nameof(Search), controllerName, new
            {
                queryBase.nome,
                queryBase.email,
                queryBase.cargo,
                queryBase.sortBy,
                queryBase.sortDirection,
                pageNumber,
                pageSize
            }, Request.Scheme)!,
            Rel = "self",
            Method = "GET"
        });

        // prev
        if (pageNumber > 1)
        {
            result.Links.Add(new LinkDTO
            {
                Href = Url.Action(nameof(Search), controllerName, new
                {
                    queryBase.nome,
                    queryBase.email,
                    queryBase.cargo,
                    queryBase.sortBy,
                    queryBase.sortDirection,
                    pageNumber = pageNumber - 1,
                    pageSize
                }, Request.Scheme)!,
                Rel = "prev_page",
                Method = "GET"
            });
        }

        // next
        if (pageNumber * pageSize < totalCount)
        {
            result.Links.Add(new LinkDTO
            {
                Href = Url.Action(nameof(Search), controllerName, new
                {
                    queryBase.nome,
                    queryBase.email,
                    queryBase.cargo,
                    queryBase.sortBy,
                    queryBase.sortDirection,
                    pageNumber = pageNumber + 1,
                    pageSize
                }, Request.Scheme)!,
                Rel = "next_page",
                Method = "GET"
            });
        }

        return Ok(result);
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
using Microsoft.AspNetCore.Mvc;
using Zoo.Application.DTOs;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Domain.Entities;
using Zoo.Domain.Enums;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _repo;

    public EnclosuresController(IEnclosureRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enclosures = await _repo.GetAllAsync();
        var result = enclosures.Select(e => new EnclosureDto(e.Id, e.Type, e.Area, e.Capacity, e.AnimalIds.Count));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnclosureRequest request)
    {
        var enclosure = new Enclosure(request.Type, request.Area, request.Capacity);
        await _repo.AddAsync(enclosure);
        await _repo.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = enclosure.Id }, null);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var enclosure = await _repo.GetByIdAsync(id);
        if (enclosure == null) return NotFound();
        await _repo.RemoveAsync(enclosure);
        await _repo.SaveChangesAsync();
        return NoContent();
    }

    public record CreateEnclosureRequest(EnclosureType Type, double Area, int Capacity);
}
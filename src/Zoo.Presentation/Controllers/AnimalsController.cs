using Microsoft.AspNetCore.Mvc;
using Zoo.Application.DTOs;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Domain.Entities;
using Zoo.Domain.Enums;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepo;

    public AnimalsController(IAnimalRepository animalRepo)
    {
        _animalRepo = animalRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _animalRepo.GetAllAsync();
        var result = animals.Select(a => new AnimalDto(a.Id, a.Species, a.Name, a.BirthDate.ToString("yyyy-MM-dd"), a.Gender, a.FavoriteFood, a.EnclosureId));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var animal = await _animalRepo.GetByIdAsync(id);
        if (animal == null) return NotFound();
        return Ok(new AnimalDto(animal.Id, animal.Species, animal.Name, animal.BirthDate.ToString("yyyy-MM-dd"), animal.Gender, animal.FavoriteFood, animal.EnclosureId));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnimalRequest request)
    {
        var animal = new Animal(request.Species, request.Name, DateOnly.Parse(request.BirthDate), request.Gender, request.FavoriteFood, request.EnclosureId);
        await _animalRepo.AddAsync(animal);
        await _animalRepo.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = animal.Id }, null);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var animal = await _animalRepo.GetByIdAsync(id);
        if (animal == null) return NotFound();
        await _animalRepo.RemoveAsync(animal);
        await _animalRepo.SaveChangesAsync();
        return NoContent();
    }

    // DTO‑запрос создания
    public record CreateAnimalRequest(string Species, string Name, string BirthDate, AnimalGender Gender, string FavoriteFood, Guid EnclosureId);
}
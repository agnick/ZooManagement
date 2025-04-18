using Zoo.Application.DTOs;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Application.Interfaces.Services;

namespace Zoo.Application.Services;

/// <summary>
/// Сервис статистики.
/// </summary>
public class ZooStatisticsService : IZooStatisticsService
{
    private readonly IAnimalRepository _animalRepo;
    private readonly IEnclosureRepository _enclosureRepo;

    public ZooStatisticsService(IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo)
    {
        _animalRepo = animalRepo;
        _enclosureRepo = enclosureRepo;
    }

    public async Task<ZooStatisticsDto> GetStatisticsAsync()
    {
        var animals = await _animalRepo.GetAllAsync();
        var enclosures = await _enclosureRepo.GetAllAsync();
        var free = enclosures.Count(e => e.AnimalIds.Count < e.Capacity);
        return new ZooStatisticsDto(animals.Count, enclosures.Count, free);
    }
}
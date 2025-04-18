using Zoo.Application.Interfaces;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Application.Interfaces.Services;
using Zoo.Domain.Exceptions;

namespace Zoo.Application.Services;

/// <summary>
/// Сервис перемещения животных.
/// </summary>
public class AnimalTransferService : IAnimalTransferService
{
    private readonly IAnimalRepository _animalRepo;
    private readonly IEnclosureRepository _enclosureRepo;
    private readonly IEventDispatcher _dispatcher;

    public AnimalTransferService(IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo, IEventDispatcher dispatcher)
    {
        _animalRepo = animalRepo;
        _enclosureRepo = enclosureRepo;
        _dispatcher = dispatcher;
    }

    public async Task MoveAsync(Guid animalId, Guid targetEnclosureId)
    {
        var animal = await _animalRepo.GetByIdAsync(animalId) ?? throw new DomainException("Животное не найдено.");
        var target = await _enclosureRepo.GetByIdAsync(targetEnclosureId) ?? throw new DomainException("Целевой вольер не найден.");
        var current = await _enclosureRepo.GetByIdAsync(animal.EnclosureId) ?? throw new DomainException("Текущий вольер не найден.");

        // Проверка вместимости
        if (target.AnimalIds.Count >= target.Capacity)
            throw new DomainException("Целевой вольер заполнен.");

        // Изменяем вольеры
        current.RemoveAnimal(animal);
        target.AddAnimal(animal);

        animal.MoveToEnclosure(target.Id);

        // Сохраняем состояние
        await _enclosureRepo.SaveChangesAsync();
        await _animalRepo.SaveChangesAsync();

        // Публикация событий
        await _dispatcher.DispatchAsync(animal.DomainEvents);
        animal.ClearDomainEvents();
    }
}
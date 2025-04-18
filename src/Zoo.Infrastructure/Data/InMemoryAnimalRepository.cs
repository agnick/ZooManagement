using System.Collections.Concurrent;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Domain.Entities;

namespace Zoo.Infrastructure.Data;

/// <summary>
/// In‑memory хранилище животных (потокобезопасное).
/// </summary>
public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly ConcurrentDictionary<Guid, Animal> _storage = new();

    public Task AddAsync(Animal animal)
    {
        _storage[animal.Id] = animal;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Animal>> GetAllAsync() => Task.FromResult<IReadOnlyCollection<Animal>>(_storage.Values.ToList());

    public Task<Animal?> GetByIdAsync(Guid id) => Task.FromResult(_storage.TryGetValue(id, out var a) ? a : null);

    public Task RemoveAsync(Animal animal)
    {
        _storage.TryRemove(animal.Id, out _);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync() => Task.CompletedTask; // Для in‑memory ничего не делаем.
}
using System.Collections.Concurrent;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Domain.Entities;

namespace Zoo.Infrastructure.Data;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly ConcurrentDictionary<Guid, Enclosure> _storage = new();

    public Task AddAsync(Enclosure enclosure)
    {
        _storage[enclosure.Id] = enclosure;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<Enclosure>> GetAllAsync() => Task.FromResult<IReadOnlyCollection<Enclosure>>(_storage.Values.ToList());

    public Task<Enclosure?> GetByIdAsync(Guid id) => Task.FromResult(_storage.TryGetValue(id, out var e) ? e : null);

    public Task RemoveAsync(Enclosure enclosure)
    {
        _storage.TryRemove(enclosure.Id, out _);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync() => Task.CompletedTask;
}
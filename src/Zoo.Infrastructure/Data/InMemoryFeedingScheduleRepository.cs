using System.Collections.Concurrent;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Domain.Entities;

namespace Zoo.Infrastructure.Data;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly ConcurrentDictionary<Guid, FeedingSchedule> _storage = new();

    public Task AddAsync(FeedingSchedule schedule)
    {
        _storage[schedule.Id] = schedule;
        return Task.CompletedTask;
    }

    public Task<FeedingSchedule?> GetByIdAsync(Guid id) => Task.FromResult(_storage.TryGetValue(id, out var s) ? s : null);

    public Task<IReadOnlyCollection<FeedingSchedule>> GetByAnimalAsync(Guid animalId) =>
        Task.FromResult<IReadOnlyCollection<FeedingSchedule>>(_storage.Values.Where(s => s.AnimalId == animalId).ToList());

    public Task RemoveAsync(FeedingSchedule schedule)
    {
        _storage.TryRemove(schedule.Id, out _);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync() => Task.CompletedTask;
}
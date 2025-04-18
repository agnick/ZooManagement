using Zoo.Domain.Entities;

namespace Zoo.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий расписаний кормления.
/// </summary>
public interface IFeedingScheduleRepository
{
    Task<FeedingSchedule?> GetByIdAsync(Guid id);
    Task AddAsync(FeedingSchedule schedule);
    Task RemoveAsync(FeedingSchedule schedule);
    Task<IReadOnlyCollection<FeedingSchedule>> GetByAnimalAsync(Guid animalId);
    Task SaveChangesAsync();
}
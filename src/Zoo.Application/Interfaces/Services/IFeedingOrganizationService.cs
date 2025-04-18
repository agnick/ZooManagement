using Zoo.Application.DTOs;

namespace Zoo.Application.Interfaces.Services;

/// <summary>
/// Организация кормлений.
/// </summary>
public interface IFeedingOrganizationService
{
    Task<IReadOnlyCollection<FeedingScheduleDto>> GetScheduleAsync();
    Task AddFeedingAsync(FeedingScheduleDto dto);
    Task MarkFedAsync(Guid scheduleId);
}
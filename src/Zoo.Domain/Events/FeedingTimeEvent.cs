using Zoo.Domain.Common;

namespace Zoo.Domain.Events;

/// <summary>
/// Событие наступления времени кормления.
/// </summary>
public sealed record FeedingTimeEvent(Guid AnimalId, Guid ScheduleId, DateTime OccurredAt) : IDomainEvent;
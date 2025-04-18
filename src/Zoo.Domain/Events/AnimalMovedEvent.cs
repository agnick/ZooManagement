using Zoo.Domain.Common;

namespace Zoo.Domain.Events;

/// <summary>
/// Событие перемещения животного между вольерами.
/// </summary>
public sealed record AnimalMovedEvent(Guid AnimalId, Guid FromEnclosureId, Guid ToEnclosureId, DateTime OccurredAt) : IDomainEvent;
using Zoo.Domain.Common;

namespace Zoo.Application.Interfaces;

/// <summary>
/// Публикация доменных событий.
/// </summary>
public interface IEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> events);
}
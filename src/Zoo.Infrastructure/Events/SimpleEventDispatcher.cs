using Zoo.Application.Interfaces;
using Zoo.Domain.Common;

namespace Zoo.Infrastructure.Events;

/// <summary>
/// Упрощённый публикационный механизм – просто выводит события в консоль.
/// </summary>
public class SimpleEventDispatcher : IEventDispatcher
{
    public Task DispatchAsync(IEnumerable<IDomainEvent> events)
    {
        foreach (var e in events)
        {
            Console.WriteLine($"[EVENT] {e}");
        }
        return Task.CompletedTask;
    }
}
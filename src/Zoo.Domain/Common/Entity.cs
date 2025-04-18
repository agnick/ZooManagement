namespace Zoo.Domain.Common;

/// <summary>
/// Базовый класс сущностей с Id и коллекцией доменных событий.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    private readonly List<IDomainEvent> _domainEvents = new();

    /// <summary>
    /// Коллекция доменных событий, произошедших с сущностью.
    /// </summary>
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// Добавить доменное событие в очередь.
    /// </summary>
    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    /// <summary>
    /// Очистить коллекцию событий после публикации.
    /// </summary>
    public void ClearDomainEvents() => _domainEvents.Clear();
}
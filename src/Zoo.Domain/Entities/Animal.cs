using Zoo.Domain.Common;
using Zoo.Domain.Enums;
using Zoo.Domain.Events;
using Zoo.Domain.Exceptions;

namespace Zoo.Domain.Entities;

/// <summary>
/// Сущность «Животное».
/// </summary>
public class Animal : Entity
{
    public string Species { get; private set; }
    public string Name { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public AnimalGender Gender { get; private set; }
    public string FavoriteFood { get; private set; }
    public HealthStatus HealthStatus { get; private set; }
    public Guid EnclosureId { get; private set; }

    // Конструктор для EF/сериализации
    private Animal() { }

    public Animal(string species, string name, DateOnly birthDate, AnimalGender gender, string favoriteFood, Guid enclosureId)
    {
        if (string.IsNullOrWhiteSpace(species)) throw new DomainException("Вид животного обязателен.");
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Кличка обязательна.");

        Species = species;
        Name = name;
        BirthDate = birthDate;
        Gender = gender;
        FavoriteFood = favoriteFood;
        HealthStatus = HealthStatus.Healthy;
        EnclosureId = enclosureId;
    }

    /// <summary>
    /// Покормить животное.
    /// </summary>
    public void Feed(string food)
    {
        // Для примера – если еда не любимая, ничего страшного.
        // Здесь могут быть дополнительные бизнес‑правила (отравление и т. д.).
    }

    /// <summary>
    /// Провести лечение.
    /// </summary>
    public void Treat()
    {
        HealthStatus = HealthStatus.Healthy;
    }

    /// <summary>
    /// Переместить животное в другой вольер.
    /// </summary>
    public void MoveToEnclosure(Guid newEnclosureId)
    {
        if (newEnclosureId == EnclosureId)
            throw new DomainException("Животное уже находится в этом вольере.");

        var previous = EnclosureId;
        EnclosureId = newEnclosureId;
        AddDomainEvent(new AnimalMovedEvent(Id, previous, newEnclosureId, DateTime.UtcNow));
    }
}
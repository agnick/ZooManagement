using Zoo.Domain.Common;
using Zoo.Domain.Enums;
using Zoo.Domain.Exceptions;

namespace Zoo.Domain.Entities;

/// <summary>
/// Сущность «Вольер».
/// </summary>
public class Enclosure : Entity
{
    public EnclosureType Type { get; private set; }
    public double Area { get; private set; }
    public int Capacity { get; private set; }

    private readonly List<Guid> _animalIds = new();
    public IReadOnlyCollection<Guid> AnimalIds => _animalIds.AsReadOnly();

    // Конструктор для EF/сериализации
    private Enclosure() { }

    public Enclosure(EnclosureType type, double area, int capacity)
    {
        if (area <= 0) throw new DomainException("Площадь должна быть положительной.");
        if (capacity <= 0) throw new DomainException("Вместимость должна быть положительной.");

        Type = type;
        Area = area;
        Capacity = capacity;
    }

    /// <summary>
    /// Добавить животное.
    /// </summary>
    public void AddAnimal(Animal animal)
    {
        if (_animalIds.Count >= Capacity)
            throw new DomainException("Вольер переполнен.");

        _animalIds.Add(animal.Id);
    }

    /// <summary>
    /// Удалить животное.
    /// </summary>
    public void RemoveAnimal(Animal animal)
    {
        _animalIds.Remove(animal.Id);
    }

    /// <summary>
    /// Провести уборку вольера.
    /// </summary>
    public void Clean()
    {
        // Бизнес‑логика уборки (для примера – просто заглушка).
    }
}
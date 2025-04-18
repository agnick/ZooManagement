using Zoo.Domain.Common;
using Zoo.Domain.Events;

namespace Zoo.Domain.Entities;

/// <summary>
/// Сущность «Расписание кормления».
/// </summary>
public class FeedingSchedule : Entity
{
    public Guid AnimalId { get; private set; }
    public TimeOnly FeedingTime { get; private set; }
    public string FoodType { get; private set; }
    public DateOnly? LastFedDate { get; private set; }

    // Конструктор для EF/сериализации
    private FeedingSchedule() { }

    public FeedingSchedule(Guid animalId, TimeOnly feedingTime, string foodType)
    {
        AnimalId = animalId;
        FeedingTime = feedingTime;
        FoodType = foodType;
    }

    /// <summary>
    /// Изменить параметры расписания.
    /// </summary>
    public void Change(TimeOnly feedingTime, string foodType)
    {
        FeedingTime = feedingTime;
        FoodType = foodType;
    }

    /// <summary>
    /// Отметить факт кормления.
    /// </summary>
    public void MarkCompleted(DateOnly date)
    {
        LastFedDate = date;
        AddDomainEvent(new FeedingTimeEvent(AnimalId, Id, DateTime.UtcNow));
    }
}
namespace Zoo.Application.DTOs;

public record FeedingScheduleDto(Guid Id, Guid AnimalId, string FeedingTime, string FoodType);
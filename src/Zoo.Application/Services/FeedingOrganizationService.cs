using Zoo.Application.DTOs;
using Zoo.Application.Interfaces;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Application.Interfaces.Services;
using Zoo.Domain.Entities;
using Zoo.Domain.Exceptions;

namespace Zoo.Application.Services;

/// <summary>
/// Сервис организации кормлений.
/// </summary>
public class FeedingOrganizationService : IFeedingOrganizationService
{
    private readonly IFeedingScheduleRepository _scheduleRepo;
    private readonly IAnimalRepository _animalRepo;
    private readonly IEventDispatcher _dispatcher;

    public FeedingOrganizationService(IFeedingScheduleRepository scheduleRepo, IAnimalRepository animalRepo, IEventDispatcher dispatcher)
    {
        _scheduleRepo = scheduleRepo;
        _animalRepo = animalRepo;
        _dispatcher = dispatcher;
    }

    public async Task<IReadOnlyCollection<FeedingScheduleDto>> GetScheduleAsync()
    {
        var allAnimals = await _animalRepo.GetAllAsync();
        var result = new List<FeedingScheduleDto>();
        foreach (var animal in allAnimals)
        {
            var schedules = await _scheduleRepo.GetByAnimalAsync(animal.Id);
            result.AddRange(schedules.Select(s => new FeedingScheduleDto(s.Id, s.AnimalId, s.FeedingTime.ToString("HH:mm:ss"), s.FoodType)));
        }
        return result;
    }

    public async Task AddFeedingAsync(FeedingScheduleDto dto)
    {
        var animal = await _animalRepo.GetByIdAsync(dto.AnimalId) ?? throw new DomainException("Животное не найдено.");
        var feedingTime = TimeOnly.Parse(dto.FeedingTime);
        var schedule = new FeedingSchedule(animal.Id, feedingTime, dto.FoodType);
        await _scheduleRepo.AddAsync(schedule);
        await _scheduleRepo.SaveChangesAsync();
    }

    public async Task MarkFedAsync(Guid scheduleId)
    {
        var schedule = await _scheduleRepo.GetByIdAsync(scheduleId) ?? throw new DomainException("Расписание не найдено.");
        schedule.MarkCompleted(DateOnly.FromDateTime(DateTime.UtcNow));
        await _scheduleRepo.SaveChangesAsync();
        await _dispatcher.DispatchAsync(schedule.DomainEvents);
        schedule.ClearDomainEvents();
    }
}
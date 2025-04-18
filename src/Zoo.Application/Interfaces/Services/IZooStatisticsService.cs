using Zoo.Application.DTOs;

namespace Zoo.Application.Interfaces.Services;

/// <summary>
/// Статистика зоопарка.
/// </summary>
public interface IZooStatisticsService
{
    Task<ZooStatisticsDto> GetStatisticsAsync();
}
using Zoo.Domain.Entities;

namespace Zoo.Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий вольеров.
/// </summary>
public interface IEnclosureRepository
{
    Task<Enclosure?> GetByIdAsync(Guid id);
    Task AddAsync(Enclosure enclosure);
    Task RemoveAsync(Enclosure enclosure);
    Task<IReadOnlyCollection<Enclosure>> GetAllAsync();
    Task SaveChangesAsync();
}
namespace Zoo.Application.Interfaces.Services;

/// <summary>
/// Перемещение животных между вольерами.
/// </summary>
public interface IAnimalTransferService
{
    Task MoveAsync(Guid animalId, Guid targetEnclosureId);
}
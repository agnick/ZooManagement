using Zoo.Domain.Enums;

namespace Zoo.Application.DTOs;

public record AnimalDto(Guid Id, string Species, string Name, string BirthDate, AnimalGender Gender, string FavoriteFood, Guid EnclosureId);
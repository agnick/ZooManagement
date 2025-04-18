using Zoo.Domain.Enums;

namespace Zoo.Application.DTOs;

public record EnclosureDto(Guid Id, EnclosureType Type, double Area, int Capacity, int CurrentCount);
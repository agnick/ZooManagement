using Zoo.Domain.Entities;
using Zoo.Domain.Enums;
using Xunit;

public class AnimalTests
{
    [Fact]
    public void Treat_Should_Set_HealthStatus_To_Healthy()
    {
        // Arrange
        var animal = new Animal("Tiger", "Sherkhan", new DateOnly(2017, 8, 12), AnimalGender.Male, "Meat", Guid.NewGuid());
        // Имитируем болезнь
        typeof(Animal).GetProperty("HealthStatus")!.SetValue(animal, HealthStatus.Sick);

        // Act
        animal.Treat();

        // Assert
        Assert.Equal(HealthStatus.Healthy, animal.HealthStatus);
    }

    [Fact]
    public void MoveToEnclosure_Should_Raise_DomainEvent()
    {
        var animal = new Animal("Bear", "Baloo", new DateOnly(2015, 5, 10), AnimalGender.Male, "Fish", Guid.NewGuid());
        var newEnclosureId = Guid.NewGuid();

        animal.MoveToEnclosure(newEnclosureId);

        Assert.Single(animal.DomainEvents);
    }
}
using Moq;
using Zoo.Application.Interfaces;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Application.Services;
using Zoo.Domain.Entities;
using Zoo.Domain.Enums;
using Xunit;

public class AnimalTransferServiceTests
{
    [Fact]
    public async Task MoveAsync_Should_Transfer_Animal()
    {
        // Arrange
        var animal = new Animal("Wolf", "Akela", new DateOnly(2018, 3, 3), AnimalGender.Male, "Meat", Guid.NewGuid());
        var source = new Enclosure(EnclosureType.Predator, 30, 5);
        typeof(Animal).GetProperty("EnclosureId")!.SetValue(animal, source.Id);
        source.AddAnimal(animal);
        var target = new Enclosure(EnclosureType.Predator, 30, 5);

        var animalRepo = new Mock<IAnimalRepository>();
        animalRepo.Setup(r => r.GetByIdAsync(animal.Id)).ReturnsAsync(animal);
        animalRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var enclosureRepo = new Mock<IEnclosureRepository>();
        enclosureRepo.Setup(r => r.GetByIdAsync(source.Id)).ReturnsAsync(source);
        enclosureRepo.Setup(r => r.GetByIdAsync(target.Id)).ReturnsAsync(target);
        enclosureRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

        var dispatcher = new Mock<IEventDispatcher>();
        dispatcher.Setup(d => d.DispatchAsync(It.IsAny<IEnumerable<Zoo.Domain.Common.IDomainEvent>>())).Returns(Task.CompletedTask);

        var service = new AnimalTransferService(animalRepo.Object, enclosureRepo.Object, dispatcher.Object);

        // Act
        await service.MoveAsync(animal.Id, target.Id);

        // Assert
        Assert.Contains(animal.Id, target.AnimalIds);
        Assert.Empty(source.AnimalIds);
    }
}
using Moq;
using Zoo.Application.Interfaces.Repositories;
using Zoo.Application.Services;
using Zoo.Domain.Entities;
using Zoo.Domain.Enums;
using Xunit;

public class ZooStatisticsServiceTests
{
    [Fact]
    public async Task GetStatisticsAsync_Should_Return_Correct_Numbers()
    {
        // Arrange
        var enclosure = new Enclosure(EnclosureType.Terrarium, 20, 2);
        var animal = new Animal("Snake", "Kaa", new DateOnly(2019, 5, 5), AnimalGender.Female, "Mice", enclosure.Id);
        enclosure.AddAnimal(animal);

        var animalRepo = new Mock<IAnimalRepository>();
        animalRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Animal> { animal });

        var enclosures = new List<Enclosure> { enclosure };
        var encRepo = new Mock<IEnclosureRepository>();
        encRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(enclosures);

        var service = new ZooStatisticsService(animalRepo.Object, encRepo.Object);

        // Act
        var stats = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(1, stats.AnimalCount);
        Assert.Equal(1, stats.EnclosuresTotal);
        Assert.Equal(1, stats.EnclosuresWithFreeSpace);
    }
}
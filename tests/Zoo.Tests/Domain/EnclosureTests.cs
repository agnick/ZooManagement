using Zoo.Domain.Entities;
using Zoo.Domain.Enums;
using Xunit;

public class EnclosureTests
{
    [Fact]
    public void AddAnimal_Should_Increase_Count()
    {
        var enclosure = new Enclosure(EnclosureType.Predator, 50, 2);
        var animal = new Animal("Lion", "Simba", new DateOnly(2020, 1, 1), AnimalGender.Male, "Meat", enclosure.Id);

        enclosure.AddAnimal(animal);

        Assert.Single(enclosure.AnimalIds);
    }

    [Fact]
    public void AddAnimal_Should_Throw_When_Capacity_Exceeded()
    {
        var enclosure = new Enclosure(EnclosureType.Bird, 10, 1);
        var a1 = new Animal("Parrot", "Kesha", new DateOnly(2021, 7, 1), AnimalGender.Male, "Seeds", enclosure.Id);
        var a2 = new Animal("Parrot", "Riki", new DateOnly(2021, 7, 2), AnimalGender.Male, "Seeds", enclosure.Id);

        enclosure.AddAnimal(a1);
        Assert.ThrowsAny<Exception>(() => enclosure.AddAnimal(a2));
    }
}
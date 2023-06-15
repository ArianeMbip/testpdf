namespace ApiTestMongo.UnitTests.UnitTests.Domain.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Domain.Eleves.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class CreateEleveTests
{
    private readonly Faker _faker;

    public CreateEleveTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_create_valid_eleve()
    {
        // Arrange
        var eleveToCreate = new FakeEleveForCreationDto().Generate();
        
        // Act
        var fakeEleve = Eleve.Create(eleveToCreate);

        // Assert
        fakeEleve.Nom.Should().Be(eleveToCreate.Nom);
        fakeEleve.Note.Should().Be(eleveToCreate.Note);
    }

    [Test]
    public void queue_domain_event_on_create()
    {
        // Arrange + Act
        var fakeEleve = FakeEleve.Generate();

        // Assert
        fakeEleve.DomainEvents.Count.Should().Be(1);
        fakeEleve.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(EleveCreated));
    }
}
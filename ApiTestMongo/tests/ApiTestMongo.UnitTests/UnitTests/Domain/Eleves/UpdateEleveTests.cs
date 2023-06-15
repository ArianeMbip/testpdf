namespace ApiTestMongo.UnitTests.UnitTests.Domain.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Domain.Eleves.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

[Parallelizable]
public class UpdateEleveTests
{
    private readonly Faker _faker;

    public UpdateEleveTests()
    {
        _faker = new Faker();
    }
    
    [Test]
    public void can_update_eleve()
    {
        // Arrange
        var fakeEleve = FakeEleve.Generate();
        var updatedEleve = new FakeEleveForUpdateDto().Generate();
        
        // Act
        fakeEleve.Update(updatedEleve);

        // Assert
        fakeEleve.Nom.Should().Be(updatedEleve.Nom);
        fakeEleve.Note.Should().Be(updatedEleve.Note);
    }
    
    [Test]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeEleve = FakeEleve.Generate();
        var updatedEleve = new FakeEleveForUpdateDto().Generate();
        fakeEleve.DomainEvents.Clear();
        
        // Act
        fakeEleve.Update(updatedEleve);

        // Assert
        fakeEleve.DomainEvents.Count.Should().Be(1);
        fakeEleve.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(EleveUpdated));
    }
}
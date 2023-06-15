namespace ApiTestMongo.IntegrationTests.FeatureTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.Domain.Eleves.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class EleveQueryTests : TestBase
{
    [Test]
    public async Task can_get_existing_eleve_with_accurate_props()
    {
        // Arrange
        var fakeEleveOne = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        await InsertAsync(fakeEleveOne);

        // Act
        var query = new GetEleve.Query(fakeEleveOne.Id);
        var eleve = await SendAsync(query);

        // Assert
        eleve.Nom.Should().Be(fakeEleveOne.Nom);
        eleve.Note.Should().Be(fakeEleveOne.Note);
    }

    [Test]
    public async Task get_eleve_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var query = new GetEleve.Query(badId);
        Func<Task> act = () => SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}
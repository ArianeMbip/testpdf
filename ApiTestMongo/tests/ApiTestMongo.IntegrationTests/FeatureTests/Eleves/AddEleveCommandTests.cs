namespace ApiTestMongo.IntegrationTests.FeatureTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using ApiTestMongo.Domain.Eleves.Features;
using static TestFixture;
using SharedKernel.Exceptions;

public class AddEleveCommandTests : TestBase
{
    [Test]
    public async Task can_add_new_eleve_to_db()
    {
        // Arrange
        var fakeEleveOne = new FakeEleveForCreationDto().Generate();

        // Act
        var command = new AddEleve.Command(fakeEleveOne);
        var eleveReturned = await SendAsync(command);
        var eleveCreated = await ExecuteDbContextAsync(db => db.Eleves
            .FirstOrDefaultAsync(e => e.Id == eleveReturned.Id));

        // Assert
        eleveReturned.Nom.Should().Be(fakeEleveOne.Nom);
        eleveReturned.Note.Should().Be(fakeEleveOne.Note);

        eleveCreated.Nom.Should().Be(fakeEleveOne.Nom);
        eleveCreated.Note.Should().Be(fakeEleveOne.Note);
    }
}
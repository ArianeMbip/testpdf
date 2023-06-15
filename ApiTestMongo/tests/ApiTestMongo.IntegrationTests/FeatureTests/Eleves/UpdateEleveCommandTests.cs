namespace ApiTestMongo.IntegrationTests.FeatureTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.Domain.Eleves.Dtos;
using SharedKernel.Exceptions;
using ApiTestMongo.Domain.Eleves.Features;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UpdateEleveCommandTests : TestBase
{
    [Test]
    public async Task can_update_existing_eleve_in_db()
    {
        // Arrange
        var fakeEleveOne = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        var updatedEleveDto = new FakeEleveForUpdateDto().Generate();
        await InsertAsync(fakeEleveOne);

        var eleve = await ExecuteDbContextAsync(db => db.Eleves
            .FirstOrDefaultAsync(e => e.Id == fakeEleveOne.Id));
        var id = eleve.Id;

        // Act
        var command = new UpdateEleve.Command(id, updatedEleveDto);
        await SendAsync(command);
        var updatedEleve = await ExecuteDbContextAsync(db => db.Eleves.FirstOrDefaultAsync(e => e.Id == id));

        // Assert
        updatedEleve.Nom.Should().Be(updatedEleveDto.Nom);
        updatedEleve.Note.Should().Be(updatedEleveDto.Note);
    }
}
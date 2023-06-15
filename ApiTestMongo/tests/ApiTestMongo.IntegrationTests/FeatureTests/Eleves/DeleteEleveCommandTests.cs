namespace ApiTestMongo.IntegrationTests.FeatureTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.Domain.Eleves.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using static TestFixture;

public class DeleteEleveCommandTests : TestBase
{
    [Test]
    public async Task can_delete_eleve_from_db()
    {
        // Arrange
        var fakeEleveOne = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        await InsertAsync(fakeEleveOne);
        var eleve = await ExecuteDbContextAsync(db => db.Eleves
            .FirstOrDefaultAsync(e => e.Id == fakeEleveOne.Id));

        // Act
        var command = new DeleteEleve.Command(eleve.Id);
        await SendAsync(command);
        var eleveResponse = await ExecuteDbContextAsync(db => db.Eleves.CountAsync(e => e.Id == eleve.Id));

        // Assert
        eleveResponse.Should().Be(0);
    }

    [Test]
    public async Task delete_eleve_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteEleve.Command(badId);
        Func<Task> act = () => SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task can_softdelete_eleve_from_db()
    {
        // Arrange
        var fakeEleveOne = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        await InsertAsync(fakeEleveOne);
        var eleve = await ExecuteDbContextAsync(db => db.Eleves
            .FirstOrDefaultAsync(e => e.Id == fakeEleveOne.Id));

        // Act
        var command = new DeleteEleve.Command(eleve.Id);
        await SendAsync(command);
        var deletedEleve = await ExecuteDbContextAsync(db => db.Eleves
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == eleve.Id));

        // Assert
        deletedEleve?.IsDeleted.Should().BeTrue();
    }
}
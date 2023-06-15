namespace ApiTestMongo.FunctionalTests.FunctionalTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class UpdateEleveRecordTests : TestBase
{
    [Test]
    public async Task put_eleve_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeEleve = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        var updatedEleveDto = new FakeEleveForUpdateDto().Generate();
        await InsertAsync(fakeEleve);

        // Act
        var route = ApiRoutes.Eleves.Put.Replace(ApiRoutes.Eleves.Id, fakeEleve.Id.ToString());
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedEleveDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
namespace ApiTestMongo.FunctionalTests.FunctionalTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetEleveTests : TestBase
{
    [Test]
    public async Task get_eleve_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeEleve = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        await InsertAsync(fakeEleve);

        // Act
        var route = ApiRoutes.Eleves.GetRecord.Replace(ApiRoutes.Eleves.Id, fakeEleve.Id.ToString());
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
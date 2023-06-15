namespace ApiTestMongo.FunctionalTests.FunctionalTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class CreateEleveTests : TestBase
{
    [Test]
    public async Task create_eleve_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeEleve = new FakeEleveForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Eleves.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeEleve);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
namespace ApiTestMongo.FunctionalTests.FunctionalTests.Eleves;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.FunctionalTests.TestUtilities;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetEleveListTests : TestBase
{
    [Test]
    public async Task get_eleve_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Eleves.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
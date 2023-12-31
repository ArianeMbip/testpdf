namespace ApiTestMongo.FunctionalTests.FunctionalTests.Users;

using ApiTestMongo.SharedTestHelpers.Fakes.User;
using ApiTestMongo.FunctionalTests.TestUtilities;
using ApiTestMongo.Domain;
using SharedKernel.Domain;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

public class GetUserListTests : TestBase
{
    [Test]
    public async Task get_user_list_returns_success_using_valid_auth_credentials()
    {
        // Arrange
        

        var user = await AddNewSuperAdmin();
        FactoryClient.AddAuth(user.Identifier);

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Users.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
            
    [Test]
    public async Task get_user_list_returns_unauthorized_without_valid_token()
    {
        // Arrange
        // N/A

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Users.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
            
    [Test]
    public async Task get_user_list_returns_forbidden_without_proper_scope()
    {
        // Arrange
        FactoryClient.AddAuth();

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Users.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}
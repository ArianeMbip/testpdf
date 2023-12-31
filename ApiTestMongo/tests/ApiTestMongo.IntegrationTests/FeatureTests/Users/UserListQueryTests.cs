namespace ApiTestMongo.IntegrationTests.FeatureTests.Users;

using ApiTestMongo.Domain.Users.Dtos;
using ApiTestMongo.SharedTestHelpers.Fakes.User;
using SharedKernel.Exceptions;
using ApiTestMongo.Domain.Users.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class UserListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_user_list()
    {
        // Arrange
        var fakeUserOne = FakeUser.Generate(new FakeUserForCreationDto().Generate());
        var fakeUserTwo = FakeUser.Generate(new FakeUserForCreationDto().Generate());
        var queryParameters = new UserParametersDto();

        await InsertAsync(fakeUserOne, fakeUserTwo);

        // Act
        var query = new GetUserList.Query(queryParameters);
        var users = await SendAsync(query);

        // Assert
        users.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}
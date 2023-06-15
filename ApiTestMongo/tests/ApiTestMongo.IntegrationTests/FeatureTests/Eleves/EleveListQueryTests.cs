namespace ApiTestMongo.IntegrationTests.FeatureTests.Eleves;

using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using SharedKernel.Exceptions;
using ApiTestMongo.Domain.Eleves.Features;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static TestFixture;

public class EleveListQueryTests : TestBase
{
    
    [Test]
    public async Task can_get_eleve_list()
    {
        // Arrange
        var fakeEleveOne = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        var fakeEleveTwo = FakeEleve.Generate(new FakeEleveForCreationDto().Generate());
        var queryParameters = new EleveParametersDto();

        await InsertAsync(fakeEleveOne, fakeEleveTwo);

        // Act
        var query = new GetEleveList.Query(queryParameters);
        var eleves = await SendAsync(query);

        // Assert
        eleves.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}
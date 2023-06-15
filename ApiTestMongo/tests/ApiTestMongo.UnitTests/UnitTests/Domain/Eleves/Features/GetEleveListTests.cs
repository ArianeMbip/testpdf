namespace ApiTestMongo.UnitTests.UnitTests.Domain.Eleves.Features;

using ApiTestMongo.SharedTestHelpers.Fakes.Eleve;
using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Domain.Eleves.Mappings;
using ApiTestMongo.Domain.Eleves.Features;
using ApiTestMongo.Domain.Eleves.Services;
using MapsterMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Sieve.Models;
using Sieve.Services;
using TestHelpers;
using NUnit.Framework;

public class GetEleveListTests
{
    
    private readonly SieveProcessor _sieveProcessor;
    private readonly Mapper _mapper = UnitTestUtils.GetApiMapper();
    private readonly Mock<IEleveRepository> _eleveRepository;

    public GetEleveListTests()
    {
        _eleveRepository = new Mock<IEleveRepository>();
        var sieveOptions = Options.Create(new SieveOptions());
        _sieveProcessor = new SieveProcessor(sieveOptions);
    }
    
    [Test]
    public async Task can_get_paged_list_of_eleve()
    {
        //Arrange
        var fakeEleveOne = FakeEleve.Generate();
        var fakeEleveTwo = FakeEleve.Generate();
        var fakeEleveThree = FakeEleve.Generate();
        var eleve = new List<Eleve>();
        eleve.Add(fakeEleveOne);
        eleve.Add(fakeEleveTwo);
        eleve.Add(fakeEleveThree);
        var mockDbData = eleve.AsQueryable().BuildMock();
        
        var queryParameters = new EleveParametersDto() { PageSize = 1, PageNumber = 2 };

        _eleveRepository
            .Setup(x => x.Query())
            .Returns(mockDbData);
        
        //Act
        var query = new GetEleveList.Query(queryParameters);
        var handler = new GetEleveList.Handler(_eleveRepository.Object, _mapper, _sieveProcessor);
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().HaveCount(1);
    }
}
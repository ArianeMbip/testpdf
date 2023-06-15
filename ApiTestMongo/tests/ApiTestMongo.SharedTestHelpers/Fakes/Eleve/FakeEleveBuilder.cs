namespace ApiTestMongo.SharedTestHelpers.Fakes.Eleve;

using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Domain.Eleves.Dtos;

public class FakeEleveBuilder
{
    private EleveForCreationDto _creationData = new FakeEleveForCreationDto().Generate();

    public FakeEleveBuilder WithDto(EleveForCreationDto dto)
    {
        _creationData = dto;
        return this;
    }
    
    public Eleve Build()
    {
        var result = Eleve.Create(_creationData);
        return result;
    }
}
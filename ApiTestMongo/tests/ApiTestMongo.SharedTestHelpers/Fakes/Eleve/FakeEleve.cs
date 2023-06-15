namespace ApiTestMongo.SharedTestHelpers.Fakes.Eleve;

using AutoBogus;
using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Domain.Eleves.Dtos;

public sealed class FakeEleve
{
    public static Eleve Generate(EleveForCreationDto eleveForCreationDto)
    {
        return Eleve.Create(eleveForCreationDto);
    }

    public static Eleve Generate()
    {
        return Generate(new FakeEleveForCreationDto().Generate());
    }
}
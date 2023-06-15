namespace ApiTestMongo.Domain.Eleves.Mappings;

using ApiTestMongo.Domain.Eleves.Dtos;
using ApiTestMongo.Domain.Eleves;
using Mapster;

public sealed class EleveMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Eleve, EleveDto>();
        config.NewConfig<EleveForCreationDto, Eleve>()
            .TwoWays();
        config.NewConfig<EleveForUpdateDto, Eleve>()
            .TwoWays();
    }
}
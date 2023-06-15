namespace ApiTestMongo.Domain.Eleves.Dtos;

using SharedKernel.Dtos;

public sealed class EleveParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}

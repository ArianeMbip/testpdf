namespace ApiTestMongo.Domain.Eleves.Validators;

using ApiTestMongo.Domain.Eleves.Dtos;
using FluentValidation;

public sealed class EleveForCreationDtoValidator: EleveForManipulationDtoValidator<EleveForCreationDto>
{
    public EleveForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}
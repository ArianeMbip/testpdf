namespace ApiTestMongo.Domain.Eleves.Validators;

using ApiTestMongo.Domain.Eleves.Dtos;
using FluentValidation;

public sealed class EleveForUpdateDtoValidator: EleveForManipulationDtoValidator<EleveForUpdateDto>
{
    public EleveForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
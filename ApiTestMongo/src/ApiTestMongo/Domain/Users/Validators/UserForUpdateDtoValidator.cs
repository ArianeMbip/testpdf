namespace ApiTestMongo.Domain.Users.Validators;

using ApiTestMongo.Domain.Users.Dtos;
using FluentValidation;

public sealed class UserForUpdateDtoValidator: UserForManipulationDtoValidator<UserForUpdateDto>
{
    public UserForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}
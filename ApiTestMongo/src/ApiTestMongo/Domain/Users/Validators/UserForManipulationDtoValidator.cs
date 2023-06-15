namespace ApiTestMongo.Domain.Users.Validators;

using ApiTestMongo.Domain.Users.Dtos;
using ApiTestMongo.Domain;
using FluentValidation;

public class UserForManipulationDtoValidator<T> : AbstractValidator<T> where T : UserForManipulationDto
{
    public UserForManipulationDtoValidator()
    {
        RuleFor(u => u.Identifier)
            .NotEmpty()
            .WithMessage("Please provide an identifier.");
    }
}
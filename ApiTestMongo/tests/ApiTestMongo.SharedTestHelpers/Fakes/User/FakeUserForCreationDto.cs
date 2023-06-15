namespace ApiTestMongo.SharedTestHelpers.Fakes.User;

using AutoBogus;
using ApiTestMongo.Domain;
using ApiTestMongo.Domain.Users.Dtos;
using ApiTestMongo.Domain.Roles;

public sealed class FakeUserForCreationDto : AutoFaker<UserForCreationDto>
{
    public FakeUserForCreationDto()
    {
        RuleFor(u => u.Email, f => f.Person.Email);
    }
}
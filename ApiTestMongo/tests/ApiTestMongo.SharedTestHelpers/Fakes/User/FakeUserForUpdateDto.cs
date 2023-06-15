namespace ApiTestMongo.SharedTestHelpers.Fakes.User;

using AutoBogus;
using ApiTestMongo.Domain;
using ApiTestMongo.Domain.Users.Dtos;
using ApiTestMongo.Domain.Roles;

public sealed class FakeUserForUpdateDto : AutoFaker<UserForUpdateDto>
{
    public FakeUserForUpdateDto()
    {
        RuleFor(u => u.Email, f => f.Person.Email);
    }
}
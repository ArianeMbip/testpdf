namespace ApiTestMongo.SharedTestHelpers.Fakes.User;

using AutoBogus;
using ApiTestMongo.Domain.Users;
using ApiTestMongo.Domain.Users.Dtos;

public sealed class FakeUser
{
    public static User Generate(UserForCreationDto userForCreationDto)
    {
        return User.Create(userForCreationDto);
    }

    public static User Generate()
    {
        return Generate(new FakeUserForCreationDto().Generate());
    }
}
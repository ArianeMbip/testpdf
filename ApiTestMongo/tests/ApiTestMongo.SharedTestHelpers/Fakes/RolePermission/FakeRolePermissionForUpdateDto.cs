namespace ApiTestMongo.SharedTestHelpers.Fakes.RolePermission;

using AutoBogus;
using ApiTestMongo.Domain;
using ApiTestMongo.Domain.RolePermissions.Dtos;
using ApiTestMongo.Domain.Roles;

public sealed class FakeRolePermissionForUpdateDto : AutoFaker<RolePermissionForUpdateDto>
{
    public FakeRolePermissionForUpdateDto()
    {
        RuleFor(rp => rp.Permission, f => f.PickRandom(Permissions.List()));
        RuleFor(rp => rp.Role, f => f.PickRandom(Role.ListNames()));
    }
}
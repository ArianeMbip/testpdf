namespace ApiTestMongo.Domain.RolePermissions.Mappings;

using ApiTestMongo.Domain.RolePermissions.Dtos;
using ApiTestMongo.Domain.RolePermissions;
using Mapster;

public sealed class RolePermissionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RolePermission, RolePermissionDto>();
        config.NewConfig<RolePermissionForCreationDto, RolePermission>()
            .TwoWays();
        config.NewConfig<RolePermissionForUpdateDto, RolePermission>()
            .TwoWays();
    }
}
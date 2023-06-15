namespace ApiTestMongo.Domain.RolePermissions.Services;

using ApiTestMongo.Domain.RolePermissions;
using ApiTestMongo.Databases;
using ApiTestMongo.Services;

public interface IRolePermissionRepository : IGenericRepository<RolePermission>
{
}

public sealed class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
{
    private readonly TestMongoDbContext _dbContext;

    public RolePermissionRepository(TestMongoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}

namespace ApiTestMongo.Services;

using ApiTestMongo.Databases;

public interface IUnitOfWork : IApiTestMongoService
{
    Task<int> CommitChanges(CancellationToken cancellationToken = default);
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly TestMongoDbContext _dbContext;

    public UnitOfWork(TestMongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

namespace ApiTestMongo.Domain.Eleves.Services;

using ApiTestMongo.Domain.Eleves;
using ApiTestMongo.Databases;
using ApiTestMongo.Services;

public interface IEleveRepository : IGenericRepository<Eleve>
{
}

public sealed class EleveRepository : GenericRepository<Eleve>, IEleveRepository
{
    private readonly TestMongoDbContext _dbContext;

    public EleveRepository(TestMongoDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}

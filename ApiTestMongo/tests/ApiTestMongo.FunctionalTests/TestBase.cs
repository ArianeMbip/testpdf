namespace ApiTestMongo.FunctionalTests;

using ApiTestMongo.Databases;
using ApiTestMongo;
using ApiTestMongo.Domain.Roles;
using ApiTestMongo.Domain.Users;
using ApiTestMongo.SharedTestHelpers.Fakes.User;
using AutoBogus;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Threading.Tasks;
 
public class TestBase
{
    private static IServiceScopeFactory _scopeFactory;
    private static WebApplicationFactory<Program> _factory;
    protected static HttpClient FactoryClient  { get; private set; }

    [SetUp]
    public async Task TestSetUp()
    {
        _factory = FunctionalTestFixture.Factory;
        _scopeFactory = FunctionalTestFixture.ScopeFactory;
        FactoryClient = _factory.CreateClient(new WebApplicationFactoryClientOptions());

        AutoFaker.Configure(builder =>
        {
            // configure global autobogus settings here
            builder.WithDateTimeKind(DateTimeKind.Utc)
                .WithRecursiveDepth(3)
                .WithTreeDepth(1)
                .WithRepeatCount(1);
        });
        
        // seed root user so tests won't always have user as super admin
        await AddNewSuperAdmin();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<TestMongoDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<TestMongoDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestMongoDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestMongoDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            var result = await action(scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();

            return result;
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public static Task ExecuteDbContextAsync(Func<TestMongoDbContext, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TestMongoDbContext>()));

    public static Task ExecuteDbContextAsync(Func<TestMongoDbContext, ValueTask> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TestMongoDbContext>()).AsTask());

    public static Task ExecuteDbContextAsync(Func<TestMongoDbContext, IMediator, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TestMongoDbContext>(), sp.GetService<IMediator>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<TestMongoDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TestMongoDbContext>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<TestMongoDbContext, ValueTask<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TestMongoDbContext>()).AsTask());

    public static Task<T> ExecuteDbContextAsync<T>(Func<TestMongoDbContext, IMediator, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TestMongoDbContext>(), sp.GetService<IMediator>()));

    public static Task<int> InsertAsync<T>(params T[] entities) where T : class
    {
        return ExecuteDbContextAsync(db =>
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Add(entity);
            }
            return db.SaveChangesAsync();
        });
    }

    public static async Task<User> AddNewSuperAdmin()
    {
        var user = FakeUser.Generate();
        user.AddRole(Role.SuperAdmin());
        await InsertAsync(user);
        return user;
    }

    public static async Task<User> AddNewUser(List<Role> roles)
    {
        var user = FakeUser.Generate();
        foreach (var role in roles)
            user.AddRole(role);
        
        await InsertAsync(user);
        return user;
    }
}
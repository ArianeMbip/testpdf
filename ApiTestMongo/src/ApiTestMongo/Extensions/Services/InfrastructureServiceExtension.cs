namespace ApiTestMongo.Extensions.Services;

using ApiTestMongo.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HeimGuard;
using ApiTestMongo.Services;
using ApiTestMongo.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IWebHostEnvironment env)
    {
        // DbContext -- Do Not Delete
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        if(string.IsNullOrEmpty(connectionString))
        {
            // this makes local migrations easier to manage. feel free to refactor if desired.
            connectionString = env.IsDevelopment() 
                ? "Host=localhost;Port=58289;Database=dev_dockerconfig;Username=SA;Password=#localDockerPassword#"
                : throw new Exception("DB_CONNECTION_STRING environment variable is not set.");
        }

        services.AddDbContext<TestMongoDbContext>(options =>
            options.UseNpgsql(connectionString,
                builder => builder.MigrationsAssembly(typeof(TestMongoDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());

        // Auth -- Do Not Delete
        if (!env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("AUTH_AUTHORITY");
                    options.Audience = Environment.GetEnvironmentVariable("AUTH_AUDIENCE");
                    options.RequireHttpsMetadata = !env.IsDevelopment();
                });
        }

        services.AddAuthorization(options =>
        {
        });

        //services.AddHeimGuard<UserPolicyHandler>()
        //    .MapAuthorizationPolicies()
        //    .AutomaticallyCheckPermissions();
    }
}

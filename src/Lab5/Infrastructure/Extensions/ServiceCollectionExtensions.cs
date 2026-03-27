using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Itmo.Dev.Platform.Postgres.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Migrations;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Plugins;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));

        collection.AddPlatformMigrations(typeof(Initial).Assembly);

        collection.AddSingleton<IDataSourcePlugin, MappingPlugin>();

        collection
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IAdminRepository, AdminRepository>()
            .AddScoped<IOperationRepository, OperationRepository>();

        return collection;
    }
}
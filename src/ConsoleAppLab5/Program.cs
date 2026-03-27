using Itmo.Dev.Platform.Common.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Infrastructure.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppLab5;

public static class Program
{
    public static async Task Main()
    {
        var collection = new ServiceCollection();

        collection
            .AddPlatform()
            .AddApplication()
            .AddInfrastructureDataAccess(configuration =>
            {
                configuration.Host = "localhost";
                configuration.Port = 6432;
                configuration.Username = "postgres";
                configuration.Password = "postgres";
                configuration.Database = "postgres";
                configuration.SslMode = "Prefer";
            })
            .AddPresentationConsole()
            .AddTransient<ScenarioRunner>();

        ServiceProvider provider = collection.BuildServiceProvider();

        using IServiceScope scope = provider.CreateScope();
        {
            scope.UseInfrastructureDataAccess();

            ScenarioRunner scenarioRunner = scope.ServiceProvider
                .GetRequiredService<ScenarioRunner>();

            while (true)
            {
                await scenarioRunner.Run().ConfigureAwait(false);
            }
        }
    }
}
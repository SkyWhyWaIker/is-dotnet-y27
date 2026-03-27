using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<AbstractScenarioFactory>(serviceProvider =>
        {
            ICurrentAdminService currentAdminService = serviceProvider.GetRequiredService<ICurrentAdminService>();
            ICurrentUserService currentUserService = serviceProvider.GetRequiredService<ICurrentUserService>();
            IUserService userService = serviceProvider.GetRequiredService<IUserService>();
            IOperationService operationService = serviceProvider.GetRequiredService<IOperationService>();
            IAdminService adminService = serviceProvider.GetRequiredService<IAdminService>();

            return AbstractScenarioFactory.WithAdmin(
                currentAdminService, currentUserService, userService, operationService, adminService);
        });

        return collection;
    }
}
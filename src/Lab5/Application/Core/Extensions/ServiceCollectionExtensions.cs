using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Users.User;
using Microsoft.Extensions.DependencyInjection;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection collection)
    {
        collection
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAdminService, AdminService>()
            .AddScoped<IOperationService, OperationService>();

        collection
            .AddScoped<CurrentUserManager>()
            .AddScoped<CurrentAdminManager>();

        collection
            .AddScoped<ICurrentUserService>(p => p.GetRequiredService<CurrentUserManager>())
            .AddScoped<ICurrentAdminService>(p => p.GetRequiredService<CurrentAdminManager>());

        return collection;
    }
}
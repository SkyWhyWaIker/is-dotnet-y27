using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserLogoutScenario(IUserService userService, ICurrentUserService currentUserService, IOperationService operationService) : IScenario
{
    public string Name => "User logout";

    public async Task Run()
    {
        if (currentUserService.User is not null)
        {
            await operationService
                .AddOperation(
                    new OperationData(
                        currentUserService.User.Id,
                        OperationType.Logout,
                        OperationStatus.Success,
                        DateTime.Now))
                .ConfigureAwait(false);
        }

        await userService.Logout().ConfigureAwait(false);

        AnsiConsole.WriteLine("logout successful");
    }
}
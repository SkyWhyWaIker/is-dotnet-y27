using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserNotNullScenario(ICurrentUserService currentUserService, IScenario scenario) : IScenario
{
    public string Name => scenario.Name;

    public async Task Run()
    {
        if (currentUserService.User is null)
        {
            AnsiConsole.WriteLine("User is null.");
            return;
        }

        await scenario.Run().ConfigureAwait(false);
    }
}
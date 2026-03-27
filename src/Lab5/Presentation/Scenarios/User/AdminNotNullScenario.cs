using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class AdminNotNullScenario(ICurrentAdminService adminService, IScenario scenario) : IScenario
{
    public string Name => scenario.Name;

    public async Task Run()
    {
        if (adminService.Admin is null)
        {
            AnsiConsole.WriteLine("Admin is null.");
            return;
        }

        await scenario.Run().ConfigureAwait(false);
    }
}
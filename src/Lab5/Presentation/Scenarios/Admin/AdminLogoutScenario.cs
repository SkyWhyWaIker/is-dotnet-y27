using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admin;

public class AdminLogoutScenario(IAdminService adminService, ICurrentAdminService currentAdminService, IOperationService operationService) : IScenario
{
    public string Name => "Admin logout";

    public async Task Run()
    {
        if (currentAdminService.Admin is not null)
        {
            await operationService
                .AddOperation(
                    new OperationData(
                        currentAdminService.Admin.Id,
                        OperationType.Logout,
                        OperationStatus.Success,
                        DateTime.Now))
                .ConfigureAwait(false);
        }

        await adminService.Logout().ConfigureAwait(false);

        AnsiConsole.WriteLine("logout successful");
    }
}
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admin;

public class AdminRegisterScenario(IAdminService adminService, IOperationService operationService) : IScenario
{
    public string Name => "Admin register";

    public async Task Run()
    {
        string password = AnsiConsole.Ask<string>("Enter your admin account password: ");

        RegisterResult result = await adminService.Register(password).ConfigureAwait(false);

        switch (result)
        {
            case RegisterResult.Success(var userId):

                await operationService
                    .AddOperation(
                        new OperationData(
                            userId,
                            OperationType.Register,
                            OperationStatus.Success,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine($"Admin registered successfully;\n Your id \"{userId}\" and password \"{password}\". ");

                break;
            case RegisterResult.UserExists(var userId):
                AnsiConsole.WriteLine("Admin already exists.");

                await operationService
                    .AddOperation(
                        new OperationData(
                            userId,
                            OperationType.Register,
                            OperationStatus.Failure,
                            DateTime.Now))
                    .ConfigureAwait(false);

                break;
        }
    }
}
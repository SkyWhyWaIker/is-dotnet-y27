using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admin;

public class AdminLoginScenario(IAdminService adminService, IOperationService operationService) : IScenario
{
    public string Name => "Admin login";

    public async Task Run()
    {
        Guid adminId = AnsiConsole.Ask<Guid>("Enter admin id to login:");
        string password = AnsiConsole.Ask<string>("Enter password: ");

        AdminLoginResult result = await adminService.Login(adminId, password).ConfigureAwait(false);

        switch (result)
        {
            case AdminLoginResult.SuccessCheckPassword:
                await operationService
                    .AddOperation(
                        new OperationData(
                            adminId,
                            OperationType.Login,
                            OperationStatus.Success,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine("Admin login successful");
                break;
            case AdminLoginResult.WrongPassword:

                await operationService
                    .AddOperation(
                        new OperationData(
                            adminId,
                            OperationType.Login,
                            OperationStatus.Failure,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine("Wrong password");
                AnsiConsole.WriteLine("App will be shut down");

                Environment.Exit(0);

                break;
            case AdminLoginResult.AdminNotFound:

                await operationService
                    .AddOperation(
                        new OperationData(
                            adminId,
                            OperationType.Login,
                            OperationStatus.Failure,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine("Admin not found");
                AnsiConsole.WriteLine("Try again");

                break;
        }
    }
}
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserLoginScenario(
    IUserService userService,
    IOperationService operationService) : IScenario
{
    public string Name => "User login";

    public async Task Run()
    {
        Guid userId = AnsiConsole.Ask<Guid>("Enter your Id: ");
        string pin = AnsiConsole.Ask<string>("Enter your PIN: ");

        UserLoginResult result = await userService.Login(userId, pin).ConfigureAwait(false);

        switch (result)
        {
            case UserLoginResult.Success:

                await operationService
                    .AddOperation(
                        new OperationData(
                            userId,
                            OperationType.Login,
                            OperationStatus.Success,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine("Login successful");

                break;
            case UserLoginResult.NotFound:

                await operationService
                    .AddOperation(
                        new OperationData(
                            userId,
                            OperationType.Login,
                            OperationStatus.Failure,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine("Login failed - user not found");

                break;
            case UserLoginResult.WrongPassword:

                await operationService
                    .AddOperation(
                        new OperationData(
                            userId,
                            OperationType.Login,
                            OperationStatus.Failure,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine("Login failed - wrong pin");

                break;
        }
    }
}
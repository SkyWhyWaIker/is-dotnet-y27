using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserRegisterScenario(
    IUserService userService,
    IOperationService operationService) : IScenario
{
    public string Name => "User register";

    public async Task Run()
    {
        string password = AnsiConsole.Ask<string>("Enter your admin account password: ");

        RegisterResult result = await userService.Register(pin: password).ConfigureAwait(false);

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

                AnsiConsole.WriteLine($"User registered successfully;\n Your id \"{userId}\" and pin \"{password}\". ");

                break;
            case RegisterResult.UserExists(var userId):

                await operationService
                    .AddOperation(
                        new OperationData(
                            userId,
                            OperationType.Register,
                            OperationStatus.Success,
                            DateTime.Now))
                    .ConfigureAwait(false);

                AnsiConsole.WriteLine("User already exists.");

                break;
        }
    }
}
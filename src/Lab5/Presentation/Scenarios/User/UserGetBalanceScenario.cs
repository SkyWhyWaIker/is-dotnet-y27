using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserGetBalanceScenario(
    IUserService userService,
    IOperationService operationService,
    ICurrentUserService currentUserService) : IScenario
{
    public string Name => "User get balance";

    public async Task Run()
    {
        GetBalanceResult result = await userService.GetBalance().ConfigureAwait(false);

        switch (result)
        {
            case GetBalanceResult.Success(var balance):
                if (currentUserService.User is not null)
                {
                    await operationService
                        .AddOperation(
                            new OperationData(
                                currentUserService.User.Id,
                                OperationType.GetUserBalance,
                                OperationStatus.Success,
                                DateTime.Now,
                                balance))
                        .ConfigureAwait(false);
                }

                AnsiConsole.WriteLine($"User get balance successfully. User balance : {balance}");

                break;
            case GetBalanceResult.UserNotFound:

                if (currentUserService.User is not null)
                {
                    await operationService
                        .AddOperation(new OperationData(
                            currentUserService.User.Id,
                            OperationType.GetUserBalance,
                            OperationStatus.Failure,
                            DateTime.Now))
                        .ConfigureAwait(false);
                }

                AnsiConsole.WriteLine("User get balance failed. User not found.");
                break;
        }
    }
}
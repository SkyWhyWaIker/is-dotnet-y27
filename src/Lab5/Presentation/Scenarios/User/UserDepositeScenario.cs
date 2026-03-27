using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserDepositeScenario(IUserService userService, IOperationService operationService, ICurrentUserService currentUserService) : IScenario
{
    public string Name => "User deposite";

    public async Task Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Enter amount: ");

        DepositResult result = await userService.Deposit(amount).ConfigureAwait(false);

        switch (result)
        {
            case DepositResult.UserNotFound:
                AnsiConsole.WriteLine("User Not Found");
                break;

            case DepositResult.AmountMustBePositive:

                if (currentUserService.User is not null)
                {
                    await operationService
                        .AddOperation(
                            new OperationData(
                                currentUserService.User.Id,
                                OperationType.UserDeposit,
                                OperationStatus.Failure,
                                DateTime.Now,
                                amount))
                        .ConfigureAwait(false);
                }

                AnsiConsole.WriteLine("Deposit fail - amount must be positive");
                break;
            case DepositResult.SuccessfullyDeposited:

                if (currentUserService.User is not null)
                {
                    await operationService
                        .AddOperation(
                            new OperationData(
                                currentUserService.User.Id,
                                OperationType.UserDeposit,
                                OperationStatus.Success,
                                DateTime.Now,
                                amount))
                        .ConfigureAwait(false);
                }

                AnsiConsole.WriteLine("Deposit success");
                break;
        }
    }
}
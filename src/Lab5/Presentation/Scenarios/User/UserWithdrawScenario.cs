using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserWithdrawScenario(
    IUserService userService,
    IOperationService operationService,
    ICurrentUserService currentUserService) : IScenario
{
    public string Name => "User withdraw";

    public async Task Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Enter amount: ");

        WithdrawResult result = await userService.Withdraw(amount).ConfigureAwait(false);

        switch (result)
        {
            case WithdrawResult.AmountMustBePositive:

                if (currentUserService.User is not null)
                {
                    await operationService
                        .AddOperation(
                            new OperationData(
                                currentUserService.User.Id,
                                OperationType.UserWithdraw,
                                OperationStatus.Failure,
                                DateTime.Now,
                                amount))
                        .ConfigureAwait(false);
                }

                AnsiConsole.WriteLine("Withdraw fail - amount must be positive");

                break;
            case WithdrawResult.NotEnoughMoney:

                if (currentUserService.User is not null)
                {
                    await operationService
                        .AddOperation(
                            new OperationData(
                                currentUserService.User.Id,
                                OperationType.UserWithdraw,
                                OperationStatus.Failure,
                                DateTime.Now,
                                amount))
                        .ConfigureAwait(false);
                }

                AnsiConsole.WriteLine("Withdraw fail - user not enough money");

                break;
            case WithdrawResult.SuccessfullyWithdrawn:

                if (currentUserService.User is not null)
                {
                    await operationService
                        .AddOperation(
                            new OperationData(
                                currentUserService.User.Id,
                                OperationType.UserWithdraw,
                                OperationStatus.Success,
                                DateTime.Now,
                                amount))
                        .ConfigureAwait(false);
                }

                AnsiConsole.WriteLine("Withdraw success");

                break;
            case WithdrawResult.UserNotFound:

                AnsiConsole.WriteLine("User Not Found");

                break;
        }
    }
}
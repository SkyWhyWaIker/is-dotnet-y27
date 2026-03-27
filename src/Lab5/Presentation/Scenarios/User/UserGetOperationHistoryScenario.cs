using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;
using Spectre.Console;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

public class UserGetOperationHistoryScenario(
    IOperationService operationService,
    ICurrentUserService currentUserService) : IScenario
{
    public string Name => "User get operation history";

    public async Task Run()
    {
        IEnumerable<OperationData> result = await operationService.GetUserOperationsHistory().ConfigureAwait(false);

        if (currentUserService.User is not null)
        {
            await operationService
                .AddOperation(
                    new OperationData(
                        currentUserService.User.Id,
                        OperationType.GetUserOperationHistory,
                        OperationStatus.Success,
                        DateTime.Now))
                .ConfigureAwait(false);
        }

        result.ToList().ForEach(x => AnsiConsole.WriteLine(x.ToString()));
    }
}
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Operations;

public class OperationService(IOperationRepository repository, ICurrentUserService currentManager) : IOperationService
{
    private ICurrentUserService CurrentUserManager { get; } = currentManager;

    public async Task<IEnumerable<OperationData>> GetUserOperationsHistory()
    {
        if (CurrentUserManager.User is null)
            return Enumerable.Empty<OperationData>();

        return await repository.GetUserOperationHistory(CurrentUserManager.User.Id).ConfigureAwait(false);
    }

    public async Task<IEnumerable<OperationData>> GetOperationsHistory()
    {
        return await repository.GetOperationHistory().ConfigureAwait(false);
    }

    public async Task AddOperation(OperationData operation)
    {
        await repository.AddOperationData(operation).ConfigureAwait(false);
    }
}
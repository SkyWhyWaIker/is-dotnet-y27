using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;

public interface IOperationService
{
    Task<IEnumerable<OperationData>> GetUserOperationsHistory();

    Task<IEnumerable<OperationData>> GetOperationsHistory();

    Task AddOperation(OperationData operation);
}
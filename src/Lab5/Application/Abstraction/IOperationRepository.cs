using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;

public interface IOperationRepository
{
    Task<IEnumerable<OperationData>> GetUserOperationHistory(Guid userId);

    Task<IEnumerable<OperationData>> GetOperationHistory();

    Task AddOperationData(OperationData operationData);
}
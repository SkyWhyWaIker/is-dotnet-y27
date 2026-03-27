namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Operations;

public sealed record OperationData(Guid UserId, OperationType Type, OperationStatus Status, DateTime Date, decimal Amount = decimal.Zero);
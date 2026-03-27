namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;

public abstract record DepositResult
{
    public sealed record UserNotFound : DepositResult;

    public sealed record AmountMustBePositive : DepositResult;

    public sealed record SuccessfullyDeposited : DepositResult;
}
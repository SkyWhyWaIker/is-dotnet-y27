namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;

public abstract record WithdrawResult
{
    public sealed record UserNotFound : WithdrawResult;

    public sealed record NotEnoughMoney : WithdrawResult;

    public sealed record SuccessfullyWithdrawn : WithdrawResult;

    public sealed record AmountMustBePositive : WithdrawResult;
}
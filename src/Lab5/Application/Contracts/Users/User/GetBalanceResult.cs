namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;

public abstract record GetBalanceResult
{
    public sealed record Success(decimal Balance) : GetBalanceResult;

    public sealed record UserNotFound : GetBalanceResult;
}
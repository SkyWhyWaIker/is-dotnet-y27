namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;

public abstract record RegisterResult
{
    public sealed record Success(Guid UserId) : RegisterResult;

    public sealed record UserExists(Guid UserId) : RegisterResult;
}
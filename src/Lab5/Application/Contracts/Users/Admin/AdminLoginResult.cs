namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;

public abstract record AdminLoginResult
{
    public sealed record AdminNotFound : AdminLoginResult;

    public sealed record WrongPassword : AdminLoginResult;

    public sealed record SuccessCheckPassword : AdminLoginResult;
}
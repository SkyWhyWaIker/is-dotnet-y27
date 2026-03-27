namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;

public abstract record UserLoginResult
{
    public sealed record Success : UserLoginResult;

    public sealed record NotFound : UserLoginResult;

    public sealed record WrongPassword : UserLoginResult;
}
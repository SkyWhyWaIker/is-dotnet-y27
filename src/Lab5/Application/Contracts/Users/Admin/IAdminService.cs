namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;

public interface IAdminService
{
    Task<AdminLoginResult> Login(Guid id, string password);

    Task Logout();

    Task<RegisterResult> Register(string password);
}
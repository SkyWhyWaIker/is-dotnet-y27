namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;

public interface IUserService
{
    Task<UserLoginResult> Login(Guid userId, string pin);

    Task Logout();

    Task<RegisterResult> Register(string pin);

    Task<WithdrawResult> Withdraw(decimal amount);

    Task<DepositResult> Deposit(decimal amount);

    Task<GetBalanceResult> GetBalance();
}
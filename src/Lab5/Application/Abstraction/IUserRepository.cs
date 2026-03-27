using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;

public interface IUserRepository
{
    Task<UserData?> FindUserById(Guid userId);

    Task Create(Guid userId, string pin);

    Task<decimal> GetBalance(Guid userId);

    Task Withdraw(Guid userId, decimal amount);

    Task Deposit(Guid userId, decimal amount);
}
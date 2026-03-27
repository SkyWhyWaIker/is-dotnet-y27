using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Users.User;

public class UserService(IUserRepository repository, ICurrentUserService currentManager) : IUserService
{
    private ICurrentUserService CurrentManager { get; } = currentManager;

    public async Task<UserLoginResult> Login(Guid userId, string pin)
    {
        UserData? user = await repository.FindUserById(userId).ConfigureAwait(false);

        if (user is null)
            return new UserLoginResult.NotFound();

        if (user.Pin != pin)
            return new UserLoginResult.WrongPassword();

        if (CurrentManager.User is not null)
        {
            await Logout().ConfigureAwait(false);
        }

        CurrentManager.User = user;

        return new UserLoginResult.Success();
    }

    public Task Logout()
    {
        CurrentManager.User = null;
        return Task.CompletedTask;
    }

    public async Task<RegisterResult> Register(string pin)
    {
        var userId = Guid.NewGuid();

        UserData? user = await repository.FindUserById(userId).ConfigureAwait(false);

        if (user is not null)
            return new RegisterResult.UserExists(userId);

        await repository.Create(userId, pin).ConfigureAwait(false);

        return new RegisterResult.Success(userId);
    }

    public async Task<WithdrawResult> Withdraw(decimal amount)
    {
        if (CurrentManager.User is null)
            return new WithdrawResult.UserNotFound();

        if (amount < 0)
            return new WithdrawResult.AmountMustBePositive();

        if (amount > await repository.GetBalance(CurrentManager.User.Id).ConfigureAwait(false))
            return new WithdrawResult.NotEnoughMoney();

        await repository.Withdraw(CurrentManager.User.Id, amount).ConfigureAwait(false);

        return new WithdrawResult.SuccessfullyWithdrawn();
    }

    public async Task<DepositResult> Deposit(decimal amount)
    {
        if (CurrentManager.User is null)
            return new DepositResult.UserNotFound();

        if (amount < 0)
            return new DepositResult.AmountMustBePositive();

        await repository.Deposit(CurrentManager.User.Id, amount).ConfigureAwait(false);

        return new DepositResult.SuccessfullyDeposited();
    }

    public async Task<GetBalanceResult> GetBalance()
    {
        if (CurrentManager.User is null)
            return new GetBalanceResult.UserNotFound();

        decimal result = await repository.GetBalance(CurrentManager.User.Id).ConfigureAwait(false);

        return new GetBalanceResult.Success(result);
    }
}
using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class Testing
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ICurrentUserService> _currentManagerMock;
    private readonly UserService _userService;

    public Testing()
    {
        _currentManagerMock = new Mock<ICurrentUserService>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object, new CurrentUserManager());
    }

    [Fact]
    public void Test1()
    {
        var userId = Guid.NewGuid();
        decimal initialBalance = 100m;
        decimal withdrawalAmount = 50m;
        decimal expectedBalance = initialBalance - withdrawalAmount;

        var user = new UserData(userId, "1234", initialBalance);

        _currentManagerMock.Setup(m => m.User).Returns(user);
        _userRepositoryMock.Setup(repo => repo.GetBalance(userId)).ReturnsAsync(initialBalance);
        _userRepositoryMock.Setup(repo => repo.Withdraw(userId, withdrawalAmount)).Returns(Task.CompletedTask);

        _ = _userService.Withdraw(withdrawalAmount);
        decimal resultBalance = 50m;

        Assert.Equal(expectedBalance, resultBalance);
    }

    [Fact]
    public void Test2()
    {
        var userId = Guid.NewGuid();
        decimal initialBalance = 100m;
        decimal depositAmount = 50m;
        decimal expectedBalance = initialBalance + depositAmount;

        var user = new UserData(userId, "1234", initialBalance);

        _currentManagerMock.Setup(m => m.User).Returns(user);
        _userRepositoryMock.Setup(repo => repo.GetBalance(userId)).ReturnsAsync(initialBalance);
        _userRepositoryMock.Setup(repo => repo.Deposit(userId, depositAmount)).Returns(Task.CompletedTask);

        _ = _userService.Deposit(depositAmount);

        decimal resultBalance = 150m;

        Assert.Equal(expectedBalance, resultBalance);
    }
}

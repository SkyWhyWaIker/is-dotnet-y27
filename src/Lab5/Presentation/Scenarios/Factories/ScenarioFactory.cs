using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Factories;

public class ScenarioFactory(
    ICurrentUserService currentUserService,
    IUserService userService,
    IOperationService operationService,
    IAdminService adminService,
    ICurrentAdminService currentAdminService) : AbstractScenarioFactory
{
    public override IScenario UserDeposite()
    {
        var scenario = new UserDepositeScenario(userService, operationService, currentUserService);
        return new UserNotNullScenario(currentUserService, scenario);
    }

    public override IScenario UserWithdraw()
    {
        var scenario = new UserWithdrawScenario(userService, operationService, currentUserService);
        return new UserNotNullScenario(currentUserService, scenario);
    }

    public override IScenario UserGetBalance()
    {
        var scenario = new UserGetBalanceScenario(userService, operationService, currentUserService);
        return new UserNotNullScenario(currentUserService, scenario);
    }

    public override IScenario UserGetOperationsHistory()
    {
        var scenario = new UserGetOperationHistoryScenario(operationService, currentUserService);
        return new UserNotNullScenario(currentUserService, scenario);
    }

    public override IScenario UserLogin()
    {
        return new UserLoginScenario(userService, operationService);
    }

    public override IScenario UserLogout()
    {
        var scenario = new UserLogoutScenario(userService, currentUserService, operationService);
        return new UserNotNullScenario(currentUserService, scenario);
    }

    public override IScenario UserRegister()
    {
        return new UserRegisterScenario(userService, operationService);
    }

    public override IScenario AdminLogin()
    {
        return new AdminLoginScenario(adminService, operationService);
    }

    public override IScenario AdminLogout()
    {
        var scenario = new AdminLogoutScenario(adminService, currentAdminService, operationService);
        return new AdminNotNullScenario(currentAdminService, scenario);
    }

    public override IScenario AdminRegister()
    {
        return new AdminRegisterScenario(adminService, operationService);
    }
}
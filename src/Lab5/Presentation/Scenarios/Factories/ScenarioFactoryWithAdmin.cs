using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.User;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Factories;

public class ScenarioFactoryWithAdmin(
    ICurrentAdminService currentAdminService,
    ICurrentUserService currentUserService,
    IUserService userService,
    IOperationService operationService,
    IAdminService adminService) : AbstractScenarioFactory
{
    public override IScenario UserDeposite()
    {
        var scenario = new UserDepositeScenario(userService, operationService, currentUserService);
        var scenarioWithUser = new UserNotNullScenario(currentUserService, scenario);
        return new AdminNotNullScenario(currentAdminService, scenarioWithUser);
    }

    public override IScenario UserWithdraw()
    {
        var scenario = new UserWithdrawScenario(userService, operationService, currentUserService);
        var scenarioWithUser = new UserNotNullScenario(currentUserService, scenario);
        return new AdminNotNullScenario(currentAdminService, scenarioWithUser);
    }

    public override IScenario UserGetBalance()
    {
        var scenario = new UserGetBalanceScenario(userService, operationService, currentUserService);
        var scenarioWithUser = new UserNotNullScenario(currentUserService, scenario);
        return new AdminNotNullScenario(currentAdminService, scenarioWithUser);
    }

    public override IScenario UserGetOperationsHistory()
    {
        var scenario = new UserGetOperationHistoryScenario(operationService, currentUserService);
        var scenarioWithUser = new UserNotNullScenario(currentUserService, scenario);
        return new AdminNotNullScenario(currentAdminService, scenarioWithUser);
    }

    public override IScenario UserLogin()
    {
        var scenarioWithUser = new UserLoginScenario(userService, operationService);
        return new AdminNotNullScenario(currentAdminService, scenarioWithUser);
    }

    public override IScenario UserLogout()
    {
        var scenario = new UserLogoutScenario(userService, currentUserService, operationService);
        var scenarioWithUser = new UserNotNullScenario(currentUserService, scenario);
        return new AdminNotNullScenario(currentAdminService, scenarioWithUser);
    }

    public override IScenario UserRegister()
    {
        var scenarioWithUser = new UserRegisterScenario(userService, operationService);
        return new AdminNotNullScenario(currentAdminService, scenarioWithUser);
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
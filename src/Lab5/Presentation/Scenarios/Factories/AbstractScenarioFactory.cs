using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Operations;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation.Scenarios.Factories;

public abstract class AbstractScenarioFactory
{
    public static AbstractScenarioFactory WithAdmin(
        ICurrentAdminService currentAdminService,
        ICurrentUserService currentUserService,
        IUserService userService,
        IOperationService operationService,
        IAdminService adminService)
    {
        return new ScenarioFactoryWithAdmin(
            currentAdminService,
            currentUserService,
            userService,
            operationService,
            adminService);
    }

    public static AbstractScenarioFactory Default(
        ICurrentUserService currentUserService,
        IUserService userService,
        IOperationService operationService,
        IAdminService adminService,
        ICurrentAdminService currentAdminService)
    {
        return new ScenarioFactory(currentUserService, userService, operationService, adminService, currentAdminService);
    }

    public abstract IScenario UserDeposite();

    public abstract IScenario UserWithdraw();

    public abstract IScenario UserGetBalance();

    public abstract IScenario UserGetOperationsHistory();

    public abstract IScenario UserLogin();

    public abstract IScenario UserLogout();

    public abstract IScenario UserRegister();

    public abstract IScenario AdminLogin();

    public abstract IScenario AdminLogout();

    public abstract IScenario AdminRegister();
}
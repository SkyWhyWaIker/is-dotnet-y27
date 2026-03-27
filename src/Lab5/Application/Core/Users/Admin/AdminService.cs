using Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Users.Admin;

public class AdminService(IAdminRepository repository, ICurrentAdminService currentManager) : IAdminService
{
    private ICurrentAdminService CurrentManager { get; } = currentManager;

    public async Task<AdminLoginResult> Login(Guid id, string password)
    {
        AdminData? admin = await repository.FindAdminById(id).ConfigureAwait(false);

        if (admin is null)
            return new AdminLoginResult.AdminNotFound();

        CurrentManager.Admin = admin;

        if (CurrentManager.Admin.Password != password)
            return new AdminLoginResult.WrongPassword();

        return new AdminLoginResult.SuccessCheckPassword();
    }

    public Task Logout()
    {
        CurrentManager.Admin = null;
        return Task.CompletedTask;
    }

    public async Task<RegisterResult> Register(string password)
    {
        var userId = Guid.NewGuid();

        AdminData? admin = await repository.FindAdminById(userId).ConfigureAwait(false);

        if (admin is not null)
            return new RegisterResult.UserExists(userId);

        await repository.Create(userId, password).ConfigureAwait(false);

        return new RegisterResult.Success(userId);
    }
}
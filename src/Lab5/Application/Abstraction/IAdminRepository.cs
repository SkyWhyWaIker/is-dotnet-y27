using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Abstraction;

public interface IAdminRepository
{
    Task<AdminData?> FindAdminById(Guid userId);

    Task Create(Guid userId, string password);
}
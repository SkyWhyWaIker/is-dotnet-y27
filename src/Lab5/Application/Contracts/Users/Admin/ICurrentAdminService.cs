using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;

public interface ICurrentAdminService
{
    AdminData? Admin { get; set; }
}
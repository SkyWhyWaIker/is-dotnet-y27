using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.Admin;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Users.Admin;

public class CurrentAdminManager : ICurrentAdminService
{
    public AdminData? Admin { get; set; }
}
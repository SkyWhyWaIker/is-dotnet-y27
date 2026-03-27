using Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Core.Users.User;

public class CurrentUserManager : ICurrentUserService
{
    public UserData? User { get; set; }
}
using Itmo.ObjectOrientedProgramming.Lab5.Application.Models.Users;

namespace Itmo.ObjectOrientedProgramming.Lab5.Application.Contracts.Users.User;

public interface ICurrentUserService
{
    UserData? User { get; set; }
}
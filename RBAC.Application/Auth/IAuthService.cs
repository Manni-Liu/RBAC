using RBAC.Domain.Entities;

namespace RBAC.Application.Auth;

public interface IAuthService
{
    Task<User> ValidateUserAsync(string username, string password);
}

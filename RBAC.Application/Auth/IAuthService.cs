using RBAC.Domain.Entities;

namespace RBAC.Application.Auth;

public interface IAuthService
{
    Task<User> ValidateUserAsync(
        long tenantId,
        string username,
        string password);
}

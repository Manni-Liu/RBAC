using RBAC.Domain.Entities;

namespace RBAC.Application.UserRoles;

public interface IUserRoleRepository
{
    Task AssignAsync(UserRole userRole);
    Task<bool> ExistsAsync(long userId, long roleId);
}
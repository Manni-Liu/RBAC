using RBAC.Application.Common;
using RBAC.Domain.Entities;

namespace RBAC.Application.UserRoles;

public class UserRoleService
{
    private readonly IUserRoleRepository _repo;

    public UserRoleService(IUserRoleRepository repo)
    {
        _repo = repo;
    }

    public async Task AssignRoleAsync(long userId, long roleId)
    {
        var exists = await _repo.ExistsAsync(userId, roleId);
        if (exists)
        {
            throw new BusinessException(
                "ROLE_ALREADY_ASSIGNED",
                "User already has this role");
        }

        await _repo.AssignAsync(
            new UserRole(userId, roleId)
        );
    }
}
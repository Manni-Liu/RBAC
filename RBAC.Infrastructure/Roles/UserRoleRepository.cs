using Microsoft.EntityFrameworkCore;
using RBAC.Application.UserRoles;
using RBAC.Domain.Entities;
using RBAC.Infrastructure.Persistence;

namespace RBAC.Infrastructure.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly RbacDbContext _db;

    public UserRoleRepository(RbacDbContext db)
    {
        _db = db;
    }

    public Task<bool> ExistsAsync(long userId, long roleId)
        => _db.UserRoles.AnyAsync(ur =>
            ur.UserId == userId && ur.RoleId == roleId);

    public async Task AssignAsync(UserRole userRole)
    {
        _db.UserRoles.Add(userRole);
        await _db.SaveChangesAsync();
    }
}
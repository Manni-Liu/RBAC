using Microsoft.EntityFrameworkCore;
using RBAC.Infrastructure.Persistence;

namespace RBAC.Application.Users;

public class UserService
{
    private readonly RbacDbContext _db;

    public UserService(RbacDbContext db)
    {
        _db = db;
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        return await _db.Users
            .Select(u => new UserDto(
                u.Id,
                u.Username,
                u.Email,
                (int)u.Status
            ))
            .ToListAsync();
    }
}

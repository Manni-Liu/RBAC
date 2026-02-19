using Microsoft.EntityFrameworkCore;
using RBAC.Infrastructure.Persistence;
using RBAC.Domain.Entities;
using RBAC.Application.Common;
using RBAC.Domain.Common.Enums;

namespace RBAC.Application.Auth;

public class AuthService : IAuthService
{
    private readonly RbacDbContext _db;

    public AuthService(RbacDbContext db)
    {
        _db = db;
    }

    public async Task<User> ValidateUserAsync(string username, string password)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user is null)
        {
            throw new BusinessException(
                "INVALID_CREDENTIALS",
                "Invalid username or password"
            );
        }

        if (user.Status != Status.Enabled)
        {
            throw new BusinessException(
                "USER_DISABLED",
                "User is disabled"
            );
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!passwordValid)
        {
            throw new BusinessException(
                "INVALID_CREDENTIALS",
                "Invalid username or password"
            );
        }

        return user;
    }

}

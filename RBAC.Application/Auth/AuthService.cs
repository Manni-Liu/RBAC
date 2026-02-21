using RBAC.Application.Interfaces;
using RBAC.Application.Common;
using RBAC.Domain.Common.Enums;
using RBAC.Domain.Entities;

namespace RBAC.Application.Auth;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> ValidateUserAsync(
        long tenantId,
        string username,
        string password)
    {
        var user = await _userRepository.GetByUsernameAsync(tenantId, username)
            ?? throw new BusinessException(
                "INVALID_CREDENTIALS",
                "Invalid username or password"
            );

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

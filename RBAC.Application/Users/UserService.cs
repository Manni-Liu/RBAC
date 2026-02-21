using BCrypt.Net;
using RBAC.Application.Common;
using RBAC.Domain.Common.Enums;
using RBAC.Domain.Entities;
using RBAC.Application.Interfaces;

namespace RBAC.Application.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserResponseDto>> GetListAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserResponseDto(
            u.Id,
            u.Username,
            u.Email,
            u.Phone,
            (int)u.Status,
            u.CreatedAt
        )).ToList();
    }

    public async Task<UserResponseDto> GetByIdAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new BusinessException("USER_NOT_FOUND", "User not found");

        return new UserResponseDto(
            user.Id,
            user.Username,
            user.Email,
            user.Phone,
            (int)user.Status,
            user.CreatedAt
        );
    }

    public async Task<long> CreateAsync(long tenantId, CreateUserDto dto)
    {
        var exists = await _userRepository.GetByUsernameAsync(tenantId, dto.Username);
        if (exists != null)
            throw new BusinessException("USERNAME_EXISTS", "Username already exists");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User(tenantId, dto.Username, hash);

        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.GetType().GetProperty("Email")!.SetValue(user, dto.Email);

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            user.GetType().GetProperty("Phone")!.SetValue(user, dto.Phone);

        await _userRepository.AddAsync(user);
        return user.Id;
    }

    public async Task UpdateAsync(long id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new BusinessException("USER_NOT_FOUND", "User not found");

        if (dto.Email != null)
            user.GetType().GetProperty("Email")!.SetValue(user, dto.Email);

        if (dto.Phone != null)
            user.GetType().GetProperty("Phone")!.SetValue(user, dto.Phone);

        if (dto.Status.HasValue)
        {
            if (dto.Status == (int)Status.Enabled)
                user.Enable();
            else
                user.Disable();
        }

        await _userRepository.UpdateAsync(user);
    }

    public async Task DisableAsync(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new BusinessException("USER_NOT_FOUND", "User not found");

        user.Disable();
        await _userRepository.UpdateAsync(user);
    }
}

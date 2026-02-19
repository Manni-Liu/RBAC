using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using RBAC.Application.Common;
using RBAC.Domain.Common.Enums;
using RBAC.Domain.Entities;
using RBAC.Infrastructure.Persistence;

namespace RBAC.Application.Users;

public class UserService
{
    private readonly RbacDbContext _db;

    public UserService(RbacDbContext db)
    {
        _db = db;
    }

    // GET /api/users
    public async Task<List<UserResponseDto>> GetListAsync()
    {
        return await _db.Users
            .OrderBy(u => u.Id)
            .Select(u => new UserResponseDto(
                u.Id,
                u.Username,
                u.Email,
                u.Phone,
                (int)u.Status,
                u.CreatedAt
            ))
            .ToListAsync();
    }

    // GET /api/users/{id}
    public async Task<UserResponseDto> GetByIdAsync(long id)
    {
        var user = await _db.Users.FindAsync(id);
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

    // POST /api/users
    public async Task<long> CreateAsync(long tenantId, CreateUserDto dto)
    {
        var exists = await _db.Users
            .AnyAsync(u => u.TenantId == tenantId && u.Username == dto.Username);

        if (exists)
            throw new BusinessException("USERNAME_EXISTS", "Username already exists");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User(tenantId, dto.Username, hash);

        if (!string.IsNullOrWhiteSpace(dto.Email))
            user.GetType().GetProperty("Email")!.SetValue(user, dto.Email);

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            user.GetType().GetProperty("Phone")!.SetValue(user, dto.Phone);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return user.Id;
    }

    // PUT /api/users/{id}
    public async Task UpdateAsync(long id, UpdateUserDto dto)
    {
        var user = await _db.Users.FindAsync(id);
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

        await _db.SaveChangesAsync();
    }

    // DELETE /api/users/{id}
    public async Task DisableAsync(long id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user == null)
            throw new BusinessException("USER_NOT_FOUND", "User not found");

        user.Disable();
        await _db.SaveChangesAsync();
    }
}

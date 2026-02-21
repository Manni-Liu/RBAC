using RBAC.Domain.Entities;

namespace RBAC.Application.Users;

public interface IUserService
{
    Task<List<UserResponseDto>> GetListAsync();

    Task<UserResponseDto> GetByIdAsync(long id);

    Task<long> CreateAsync(long tenantId, CreateUserDto dto);

    Task UpdateAsync(long id, UpdateUserDto dto);

    Task DisableAsync(long id);
}
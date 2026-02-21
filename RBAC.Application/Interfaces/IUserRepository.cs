// RBAC.Application/Users/IUserRepository.cs
using RBAC.Domain.Entities;

namespace RBAC.Application.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByUsernameAsync(long tenantId, string username);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}

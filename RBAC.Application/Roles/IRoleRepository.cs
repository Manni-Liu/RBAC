using RBAC.Domain.Entities;

namespace RBAC.Application.Roles;

public interface IRoleRepository
{
    Task<Role?> GetByCodeAsync(long tenantId, string code);
    Task<List<Role>> GetByTenantAsync(long tenantId);
    Task AddAsync(Role role);
}
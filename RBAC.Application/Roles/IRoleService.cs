using RBAC.Application.Roles.Dtos;

namespace RBAC.Application.Roles;

public interface IRoleService
{
    Task<RoleDto> CreateAsync(long tenantId, CreateRoleDto dto);
    Task<List<RoleDto>> GetByTenantAsync(long tenantId);
}
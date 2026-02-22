using RBAC.Application.Common;
using RBAC.Application.Roles.Dtos;
using RBAC.Domain.Entities;

namespace RBAC.Application.Roles;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepo;

    public RoleService(IRoleRepository roleRepo)
    {
        _roleRepo = roleRepo;
    }

    public async Task<RoleDto> CreateAsync(long tenantId, CreateRoleDto dto)
    {
        var exists = await _roleRepo.GetByCodeAsync(tenantId, dto.Code);
        if (exists != null)
        {
            throw new BusinessException(
                "ROLE_EXISTS",
                "Role code already exists in tenant");
        }

        var role = new Role
        {
            TenantId = tenantId,
            Name = dto.Name,
            Code = dto.Code,
            IsSystem = false,
            CreatedAt = DateTime.UtcNow
        };

        await _roleRepo.AddAsync(role);

        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name,
            Code = role.Code
        };
    }

    public async Task<List<RoleDto>> GetByTenantAsync(long tenantId)
    {
        var roles = await _roleRepo.GetByTenantAsync(tenantId);

        return roles.Select(r => new RoleDto
        {
            Id = r.Id,
            Name = r.Name,
            Code = r.Code
        }).ToList();
    }
}
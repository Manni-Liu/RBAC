using Microsoft.EntityFrameworkCore;
using RBAC.Application.Roles;
using RBAC.Domain.Entities;
using RBAC.Infrastructure.Persistence;

namespace RBAC.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly RbacDbContext _db;

    public RoleRepository(RbacDbContext db)
    {
        _db = db;
    }

    public Task<Role?> GetByCodeAsync(long tenantId, string code)
        => _db.Roles.FirstOrDefaultAsync(r =>
            r.TenantId == tenantId && r.Code == code);

    public Task<List<Role>> GetByTenantAsync(long tenantId)
        => _db.Roles
            .Where(r => r.TenantId == tenantId)
            .ToListAsync();

    public async Task AddAsync(Role role)
    {
        _db.Roles.Add(role);
        await _db.SaveChangesAsync();
    }
}
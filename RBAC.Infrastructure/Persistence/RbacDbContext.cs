using Microsoft.EntityFrameworkCore;
using RBAC.Domain.Entities;

namespace RBAC.Infrastructure.Persistence;

public class RbacDbContext : DbContext
{
    public RbacDbContext(DbContextOptions<RbacDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
}

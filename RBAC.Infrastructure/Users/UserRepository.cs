// RBAC.Infrastructure/Users/UserRepository.cs
using Microsoft.EntityFrameworkCore;
using RBAC.Application.Users;
using RBAC.Domain.Entities;
using RBAC.Infrastructure.Persistence;
using RBAC.Application.Interfaces;

namespace RBAC.Infrastructure.Users;

public class UserRepository : IUserRepository
{
    private readonly RbacDbContext _db;

    public UserRepository(RbacDbContext db)
    {
        _db = db;
    }

    public async Task<User?> GetByIdAsync(long id)
        => await _db.Users.FindAsync(id);

    public async Task<User?> GetByUsernameAsync(long tenantId, string username)
        => await _db.Users.FirstOrDefaultAsync(u => u.TenantId == tenantId && u.Username == username);

    public async Task<List<User>> GetAllAsync()
        => await _db.Users.OrderBy(u => u.Id).ToListAsync();

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }
}

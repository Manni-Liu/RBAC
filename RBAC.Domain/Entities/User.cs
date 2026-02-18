using RBAC.Domain.Common;
using RBAC.Domain.Common.Enums;

namespace RBAC.Domain.Entities;

public class User : TenantEntity
{
    private User() { }

    public User(long tenantId, string username, string passwordHash)
    {
        TenantId = tenantId;
        Username = username;
        PasswordHash = passwordHash;
        Status = Status.Enabled;
    }

    public string Username { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public Status Status { get; private set; }

    public void Disable() => Status = Status.Disabled;
    public void Enable() => Status = Status.Enabled;
}

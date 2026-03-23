using RBAC.Domain.Common;

namespace RBAC.Domain.Entities;

public class Role : TenantEntity
{
    private Role() { }

    public Role(long tenantId, string name, string code, bool isSystem = false)
    {
        TenantId = tenantId;
        Name = name;
        Code = code;
        IsSystem = isSystem;
        CreatedAt = DateTime.UtcNow;
    }

    public string Name { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public bool IsSystem { get; private set; }
    public new DateTime CreatedAt { get; private set; }
}
using RBAC.Domain.Common;
using RBAC.Domain.Common.Enums;

namespace RBAC.Domain.Entities;

public class Resource : TenantEntity
{
    private Resource() { }

    public Resource(long tenantId, string code, ResourceType type)
    {
        TenantId = tenantId;
        Code = code;
        Type = type;
    }

    public string Code { get; private set; } = default!;
    public string? Name { get; private set; }
    public ResourceType Type { get; private set; }
}

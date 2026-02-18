using RBAC.Domain.Common;

namespace RBAC.Domain.Entities;

public class Permission : AuditableEntity
{
    private Permission() { }

    public Permission(long resourceId, long actionId, string code)
    {
        ResourceId = resourceId;
        ActionId = actionId;
        Code = code;
    }

    public long ResourceId { get; private set; }
    public long ActionId { get; private set; }
    public string Code { get; private set; } = default!;
}

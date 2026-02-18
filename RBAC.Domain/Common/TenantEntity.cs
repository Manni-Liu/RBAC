namespace RBAC.Domain.Common;

public abstract class TenantEntity : AuditableEntity
{
    public long TenantId { get; protected set; }
}

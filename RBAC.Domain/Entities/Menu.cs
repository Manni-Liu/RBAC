using RBAC.Domain.Common;

namespace RBAC.Domain.Entities;

public class Menu : TenantEntity
{
    private Menu() { }

    public Menu(long tenantId, string name, string? path)
    {
        TenantId = tenantId;
        Name = name;
        Path = path;
    }

    public long? ParentId { get; private set; }
    public string Name { get; private set; } = default!;
    public string? Path { get; private set; }
    public string? Component { get; private set; }
    public int Sort { get; private set; }
    public bool Visible { get; private set; } = true;
}

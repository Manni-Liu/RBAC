namespace RBAC.Domain.Entities;

public class UserRole
{
    private UserRole() { }

    public UserRole(long userId, long roleId)
    {
        UserId = userId;
        RoleId = roleId;
        AssignedAt = DateTime.UtcNow;
    }

    public long UserId { get; private set; }

    public long RoleId { get; private set; }

    public DateTime AssignedAt { get; private set; }
}
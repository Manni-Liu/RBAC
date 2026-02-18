using RBAC.Domain.Entities;

namespace RBAC.Domain.Aggregates;

public class UserAggregate
{
    public User User { get; }
    private readonly List<Role> _roles = new();

    public IReadOnlyCollection<Role> Roles => _roles;

    public UserAggregate(User user)
    {
        User = user;
    }

    public void AssignRole(Role role)
    {
        if (_roles.Any(r => r.Id == role.Id))
            return;

        _roles.Add(role);
    }
}

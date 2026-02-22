namespace RBAC.Application.Roles.Dtos;

public class RoleDto
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
}
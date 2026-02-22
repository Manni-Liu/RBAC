namespace RBAC.Application.Roles.Dtos;

public class CreateRoleDto
{
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
}
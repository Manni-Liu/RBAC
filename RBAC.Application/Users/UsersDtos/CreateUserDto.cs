namespace RBAC.Application.Users;

public record CreateUserDto(
    string Username,
    string Password,
    string? Email,
    string? Phone
);

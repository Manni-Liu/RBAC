namespace RBAC.Application.Users;

public record UserDto(
    long Id,
    string Username,
    string? Email,
    int Status
);

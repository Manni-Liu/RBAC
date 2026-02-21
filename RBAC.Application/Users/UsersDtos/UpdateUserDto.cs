namespace RBAC.Application.Users;

public record UpdateUserDto(
    string? Email,
    string? Phone,
    int? Status
);


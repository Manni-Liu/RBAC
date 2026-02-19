namespace RBAC.Application.Users;

public record UserResponseDto(
    long Id,
    string Username,
    string? Email,
    string? Phone,
    int Status,
    DateTime CreatedAt
);

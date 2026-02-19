using RBAC.Domain.Entities;

namespace RBAC.Application.Auth;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}

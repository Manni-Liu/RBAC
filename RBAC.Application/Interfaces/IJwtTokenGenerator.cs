using RBAC.Domain.Entities;

namespace RBAC.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}

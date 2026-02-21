using Microsoft.AspNetCore.Mvc;
using RBAC.Application.Auth;
using RBAC.Application.Interfaces;

namespace RBAC.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthController(
        IAuthService authService,
        IJwtTokenGenerator tokenGenerator)
    {
        _authService = authService;
        _tokenGenerator = tokenGenerator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        long tenantId = 1; // from JWT / Header 
        var user = await _authService.ValidateUserAsync(tenantId, dto.Username, dto.Password);
        var token = _tokenGenerator.Generate(user);

        return Ok(new
        {
            accessToken = token
        });
    }
}

public record LoginDto(string Username, string Password);

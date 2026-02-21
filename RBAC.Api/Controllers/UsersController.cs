using Microsoft.AspNetCore.Mvc;
using RBAC.Application.Common;
using RBAC.Application.Users;
using Microsoft.AspNetCore.Authorization;

namespace RBAC.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{ 
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
        => Ok(await _service.GetListAsync());

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        // Chnage tenantId to 1 for testing, should get from JWT in real case
        var id = await _service.CreateAsync(1, dto);
        return CreatedAtAction(nameof(Get), new { id }, null);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(long id, UpdateUserDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _service.DisableAsync(id);
        return NoContent();
    }
}

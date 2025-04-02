using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthControllers(IAuthService authService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        var user = await authService.RegisterAsync(request);

        if (user is null) return BadRequest("Username already exist");

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        var token = await authService.LoginAsync(request);

        if (token is null) return BadRequest("Invalid username or password");

        return Ok(token);
    }

    [Authorize]
    [HttpGet("authenticate")]
    public ActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("You are authenticated");
    }
}
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

        Console.WriteLine("User: " + user);
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        var token = await authService.LoginAsync(request);

        if (token is null) return BadRequest("Invalid username or password");

        Console.WriteLine("Token: " + token);
        return Ok(token);
    }
}
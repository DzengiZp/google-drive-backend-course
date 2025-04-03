using Microsoft.AspNetCore.Mvc;

[Route("api/user")]
[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        var user = await userService.RegisterAsync(request);

        if (user is null) return BadRequest("Username already exist");

        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(UserDto request)
    {
        var token = await userService.LoginAsync(request);

        if (token is null) return BadRequest("Invalid username or password");

        return Ok(token);
    }
}
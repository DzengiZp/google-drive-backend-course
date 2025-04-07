// using Microsoft.AspNetCore.Mvc;

// [Route("api/user")]
// [ApiController]
// public class UserController(IUserService userService) : ControllerBase
// {
//     [HttpPost("register")]
//     public async Task<ActionResult<User>> Register(UserDto userDto)
//     {
//         var user = await userService.RegisterAsync(userDto);

//         if (user == null) return BadRequest("Register failed");

//         return Ok(user);
//     }

//     [HttpPost("login")]
//     public async Task<ActionResult<string>> Login(UserDto userDto)
//     {
//         var token = await userService.LoginAsync(userDto);

//         if (token is null) return BadRequest("Invalid username or password");

//         return Ok(token);
//     }
// }
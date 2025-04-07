// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;

// public class UserService(IUserRepository userRepository, IConfiguration configuration) : IUserService
// {
//     public async Task<string?> LoginAsync(UserDto userDto)
//     {
//         try
//         {
//             var user = await userRepository.GetUserByUsernameAsync(userDto.Username);
//             if (user == null)
//                 throw new Exception("Username or password is not valid");

//             if (!CheckHashedPassword(userDto.Password, user.PasswordHash))
//                 throw new Exception("Username or password is not valid");

//             return CreateToken(user);
//         }
//         catch (Exception ex)
//         {
//             throw new Exception("Error: ", ex);
//         }
//     }

//     public async Task<User?> RegisterAsync(UserDto userDto)
//     {
//         try
//         {
//             if (!(await userRepository.GetUserByUsernameAsync(userDto.Username) == null))
//                 throw new Exception("Username already exists");

//             var user = new User
//             {
//                 Username = userDto.Username,
//                 PasswordHash = HashPassword(userDto.Password)
//             };

//             await userRepository.AddUserAsync(user);
//             await userRepository.SaveChangesAsync();

//             return user;
//         }
//         catch (Exception ex)
//         {
//             throw new Exception("Error: ", ex);
//         }
//     }
//     private string CreateToken(User user)
//     {
//         var claims = new List<Claim>
//         {
//             new Claim(ClaimTypes.Name, user.Username),
//             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
//         };

//         var key = new SymmetricSecurityKey(
//             Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
//         );

//         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

//         var tokenDescriptor = new JwtSecurityToken(
//             issuer: configuration.GetValue<string>("AppSettings:Issuer"),
//             audience: configuration.GetValue<string>("AppSettings:Audience"),
//             claims: claims,
//             expires: DateTime.UtcNow.AddDays(1),
//             signingCredentials: creds
//         );

//         return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
//     }

//     private static string HashPassword(string password) // Ask
//     {
//         return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
//     }

//     private static bool CheckHashedPassword(string password, string hashedPassword) // Ask
//     {
//         return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
//     }
// }
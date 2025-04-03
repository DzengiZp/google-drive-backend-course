using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class UserService(ApplicationDbContext context, IConfiguration configuration) : IUserService
{
    public async Task<string?> LoginAsync(UserDto request)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
        if (user is null) return null;

        if (!CheckHashedPassword(request.Password, user.PasswordHash))
        {
            return null;
        }

        return CreateToken(user);
    }

    public async Task<User?> RegisterAsync(UserDto request)
    {
        if (await context.Users.AnyAsync(u => u.Username == request.Username))
        {
            return null;
        }

        var user = new User();

        var hashedPassword = HashPassword(request.Password);

        user.Username = request.Username;
        user.PasswordHash = hashedPassword;

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("AppSettings:Issuer"),
            audience: configuration.GetValue<string>("AppSettings:Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private static string HashPassword(string password) // FRÅGA WILLIAM OM DESSA SKA VARA HÄR
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    private static bool CheckHashedPassword(string password, string hashedPassword) // FRÅGA WILLIAM OM DESSA SKA VARA HÄR
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
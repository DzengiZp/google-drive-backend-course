
using Microsoft.EntityFrameworkCore;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task AddUserAsync(User user) // Ask
    {
        await context.Users.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
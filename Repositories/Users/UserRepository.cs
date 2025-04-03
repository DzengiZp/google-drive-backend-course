
public class UserRepository : IUserRepository
{
    public Task<User> CreateAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<User> LoginAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }
}
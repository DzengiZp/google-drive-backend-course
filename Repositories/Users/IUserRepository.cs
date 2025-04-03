public interface IUserRepository
{
    Task<User> CreateAsync(UserDto userDto);
    Task<User> LoginAsync(UserDto userDto);
}
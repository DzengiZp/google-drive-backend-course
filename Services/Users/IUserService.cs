public interface IUserService
{
    Task<string?> LoginAsync(UserDto userDto);
    Task<User?> RegisterAsync(UserDto userDto);
}
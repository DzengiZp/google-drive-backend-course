// public class User
// {
//     public Guid Id { get; set; }
//     public string Username { get; set; } = string.Empty;
//     public string PasswordHash { get; set; } = string.Empty;
//     public List<Folder> Folders { get; set; } = [];
//     public List<File> Files { get; set; } = [];
// }

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{

}
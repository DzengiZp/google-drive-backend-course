public class FileRepository(ApplicationDbContext context) : IFileRepository
{
    public async Task<Folder?> CheckIfFolderExistsAsync(int folderId)
    {
        var folderExists = await context.Folders.FindAsync(folderId);
        if (folderExists == null) return null;

        return folderExists;
    }

    public async Task<User?> CheckIfUserExistsAsync(Guid userId)
    {
        var userExists = await context.Users.FindAsync(userId);
        if (userExists == null) return null;

        return userExists;
    }

    public async Task<File?> DeleteFileByIdAsync(int id)
    {
        var file = await context.Files.FindAsync(id);
        if (file == null) return null;

        context.Files.Remove(file);
        await context.SaveChangesAsync();

        return file;
    }

    public async Task<File?> DownloadFileByIdAsync(int id)
    {
        var file = await context.Files.FindAsync(id);
        if (file is null) return null;

        return file;
    }

    public async Task<File?> GetFileByIdAsync(int id)
    {
        var file = await context.Files.FindAsync(id);
        if (file is null) return null;

        return file;
    }

    public async Task<File> UploadFileAsync(File file)
    {
        await context.Files.AddAsync(file);
        await context.SaveChangesAsync();

        return file;
    }
}
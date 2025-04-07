using Microsoft.EntityFrameworkCore;

public class FileRepository(ApplicationDbContext context) : IFileRepository
{
    public async Task<Folder?> CheckIfFolderExistsAsync(int folderId)
    {
        return await context.Folders.FindAsync(folderId) ?? throw new Exception("Folder doesn't exist"); //Either bool or change name.
    }

    public async Task<User?> CheckIfUserExistsAsync(Guid userId)
    {
        return await context.Users.FindAsync(userId) ?? throw new Exception("User doesn't exist");
    }

    public async Task<File> UploadFileAsync(File file)
    {
        await context.Files.AddAsync(file);
        await context.SaveChangesAsync();

        return file;
    }

    public async Task<File?> DeleteFileByIdAsync(int id)
    {
        var file = await context.Files.FindAsync(id) ?? throw new Exception("File doesn't exist");

        context.Files.Remove(file);
        await context.SaveChangesAsync();

        return file;
    }

    public async Task<File?> DownloadFileByIdAsync(int id)
    {
        return await context.Files.FindAsync(id) ?? throw new Exception("File doesn't exist");
    }

    public async Task<IEnumerable<File>> GetAllFilesAsync()
    {
        return await context.Files.ToListAsync() ?? throw new Exception("No files");
    }

    public async Task<File?> GetFileByIdAsync(int id)
    {
        return await context.Files.FindAsync(id) ?? throw new Exception("File doesn't exist");
    }
}
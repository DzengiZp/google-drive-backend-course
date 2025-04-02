
using Microsoft.EntityFrameworkCore;

public class FileRepository(ApplicationDbContext _context) : IFileRepository
{
    public async Task<Folder?> CheckIfFolderExistsAsync(int folderId)
    {
        var folderExists = await _context.Folders.FindAsync(folderId);
        if (folderExists == null) return null;

        return folderExists;
    }

    public async Task<User?> CheckIfUserExistsAsync(Guid userId)
    {
        var userExists = await _context.Users.FindAsync(userId);
        if (userExists == null) return null;

        return userExists;
    }

    public async Task<File?> DeleteFileByIdAsync(int id)
    {
        var file = await _context.Files.FindAsync(id);
        if (file == null) return null;

        _context.Files.Remove(file);
        await _context.SaveChangesAsync();

        return file;
    }

    public async Task<File?> DownloadFileByIdAsync(int id)
    {
        var file = await _context.Files.FindAsync(id);
        if (file is null) return null;

        return file;
    }

    public async Task<File?> GetFileByIdAsync(int id)
    {
        var file = await _context.Files.FindAsync(id);
        if (file is null) return null;

        return file;
    }

    public async Task<File> UploadFileAsync(File file)
    {
        await _context.Files.AddAsync(file);
        await _context.SaveChangesAsync();

        return file;
    }
}
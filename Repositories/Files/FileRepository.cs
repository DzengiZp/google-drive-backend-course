using Microsoft.EntityFrameworkCore;

public class FileRepository(ApplicationDbContext context) : IFileRepository
{
    public async Task<File> UploadFileAsync(File file)
    {
        await context.Files.AddAsync(file);
        await context.SaveChangesAsync();

        return file;
    }

    public async Task<IEnumerable<File>> GetAllFilesAsync(string userId)
    {
        return await context.Files.Where(f => f.UserId == userId).ToListAsync();
    }

    public async Task<File?> DownloadFileByNameAsync(int id)
    {
        return await context.Files.FindAsync(id) ?? throw new Exception("File doesn't exist");
    }

    public async Task<File?> DeleteFileByNameAsync(int id)
    {
        var file = await context.Files.FindAsync(id) ?? throw new Exception("File doesn't exist");

        context.Files.Remove(file);
        await context.SaveChangesAsync();

        return file;
    }
}
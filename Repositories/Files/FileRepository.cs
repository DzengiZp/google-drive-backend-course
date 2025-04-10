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
        return await context.Files.Where(file => file.UserId == userId).ToListAsync();
    }

    public async Task<File?> DownloadFileByNameAsync(string userId)
    {
        return await context.Files.Where(file => file.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task DeleteFileByNameAsync(string userId)
    {
        await context.Files.Where(file => file.UserId == userId).ExecuteDeleteAsync();
    }
}
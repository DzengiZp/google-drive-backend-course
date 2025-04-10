using Microsoft.EntityFrameworkCore;

public class FileRepository(ApplicationDbContext context) : IFileRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<File> UploadFileAsync(File file)
    {
        await context.Files.AddAsync(file);
        await context.SaveChangesAsync();

        return file;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<File>> GetAllFilesAsync(string userId)
    {
        return await context.Files.Where(file => file.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<File?> DownloadFileByNameAsync(string userId)
    {
        return await context.Files.Where(file => file.UserId == userId).FirstOrDefaultAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task DeleteFileByNameAsync(string userId)
    {
        await context.Files.Where(file => file.UserId == userId).ExecuteDeleteAsync();
    }
}
using Microsoft.EntityFrameworkCore;

public class FolderRepository(ApplicationDbContext context) : IFolderRepository
{
    public async Task<Folder> CreateForUserAsync(Folder folder)
    {
        await context.Folders.AddAsync(folder);
        await context.SaveChangesAsync();

        return folder;
    }

    public async Task<IEnumerable<Folder?>> GetAllForUserAsync(string userId)
    {
        return await context.Folders.Where(f => f.UserId == userId).ToListAsync();
    }

    public async Task<Folder?> DeleteByFolderNameAsync(string folderName)
    {
        var folder = await context.Folders.FindAsync(folderName);

        if (folder is null) return null;

        context.Folders.Remove(folder);
        await context.SaveChangesAsync();

        return folder;
    }
}
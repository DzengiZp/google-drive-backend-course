using Microsoft.EntityFrameworkCore;

public class FolderRepository(ApplicationDbContext context) : IFolderRepository
{
    public async Task<Folder> CreateAsync(Folder folder)
    {
        if (await context.Users.FindAsync(folder.UserId) == null) throw new Exception("User not found");

        await context.Folders.AddAsync(folder);
        await context.SaveChangesAsync();

        return folder;
    }

    public async Task<Folder> DeleteByIdAsync(int id)
    {
        var folder = await context.Folders.FindAsync(id) ?? throw new Exception("Folder doesn't exist");

        context.Folders.Remove(folder);
        await context.SaveChangesAsync();

        return folder;
    }

    public async Task<IEnumerable<Folder>> GetAllAsync()
    {
        return await context.Folders.ToListAsync() ?? throw new Exception("No folders exist");
    }

    public async Task<Folder> GetByIdAsync(int id)
    {
        return await context.Folders.FindAsync(id) ?? throw new Exception("Folder doesn't exist");
    }
}
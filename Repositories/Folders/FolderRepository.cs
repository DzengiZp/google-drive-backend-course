using Microsoft.EntityFrameworkCore;

public class FolderRepository(ApplicationDbContext context) : IFolderRepository
{
    /// <summary>
    /// Creates and saves a folder object in the database.
    /// </summary>
    /// <param name="folder">Recieves the folder object to be created.</param>
    /// <returns>The created folder object.</returns>
    public async Task<Folder> CreateFolderInDbAsync(Folder folder)
    {
        await context.Folders.AddAsync(folder);
        await context.SaveChangesAsync();

        return folder;
    }

    /// <summary>
    /// Retrieves the ID of a folder and its content from the database by the folder name and user ID that owns the folder.
    /// </summary>
    /// <param name="folderName">Specifies the name of the folder to be found.</param>
    /// <param name="userId">Specifies the ID of the user who owns the folder.</param>
    /// <returns>The id of the found folder.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the folder is not found in the database.</exception>
    public async Task<Guid> GetFolderFromDbAsync(string folderName, string userId)
    {
        var folder = await context.Folders.FirstOrDefaultAsync(f => f.FolderName == folderName && f.UserId == userId)
            ?? throw new ArgumentNullException($"\"{folderName}\" was not found", "Folder name");

        return folder.Id;
    }

    /// <summary>
    /// Retrieves the folders from the database that belongs to the user based on user ID.
    /// </summary>
    /// <param name="userId">Specifies the ID of the user who owns the folder.</param>
    /// <returns>A list of all the folders found in the database or an empty list if no folders are found.</returns>
    public async Task<IEnumerable<Folder?>> GetAllFoldersFromDbAsync(string userId)
    {
        return await context.Folders.Where(f => f.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// Deletes a folder from the database based on the folder name and the user ID who owns the folder.
    /// </summary>
    /// <param name="folderName">Specifies the folder name to deleted from the database.</param>
    /// <param name="userId">Specifies the ID of the user who owns the folder.</param>
    /// <exception cref="DirectoryNotFoundException">Throws if no folder has been deleted in the database.</exception>
    public async Task DeleteFolderFromDbAsync(string folderName, string userId)
    {
        var rowsDeleted = await context.Folders.Where(f => f.FolderName == folderName && f.UserId == userId).ExecuteDeleteAsync();

        if (rowsDeleted == 0) throw new DirectoryNotFoundException("Folder not found in database");
    }
}
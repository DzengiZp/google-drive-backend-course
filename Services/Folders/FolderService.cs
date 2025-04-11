public class FolderService(IFolderRepository folderRepo) : IFolderService
{
    /// <summary>
    /// Creates a new folder for the specified user and saves it to the database.
    /// </summary>
    /// <param name="folderName">Specifies the name of the folder object to be created.</param>
    /// <param name="userId">Specifies the ID of the user who owns the folder.</param>
    public async Task CreateFolderAsync(string folderName, string userId)
    {
        // COMMENT: Kan behöva validering av filnamn (om inte ASP.NET gör det?)
        var folder = new Folder
        {
            Id = Guid.NewGuid(),
            FolderName = folderName,
            UserId = userId,
        };

        await folderRepo.CreateFolderInDbAsync(folder);
    }

    /// <summary>
    /// Retrieves all the folders from the database for the specified user ID and returns it.
    /// </summary>
    /// <param name="userId">Specifies the ID of the user who owns the folder.</param>
    /// <returns>The retrieved folders for the specified user ID.</returns>
    public async Task<IEnumerable<Folder?>> GetAllFoldersAsync(string userId)
    {
        return await folderRepo.GetAllFoldersFromDbAsync(userId);
    }

    /// <summary>
    /// Deletes the folders from the database based on the specified folder name and user ID.
    /// </summary>
    /// <param name="folderName">Specifies the name of the folder to be deleted.</param>
    /// <param name="userId">Specifies the ID of the user who owns the folder.</param>
    /// <returns></returns>
    public async Task DeleteFolderAsync(string folderName, string userId)
    {
        await folderRepo.DeleteFolderFromDbAsync(folderName, userId);
    }
}

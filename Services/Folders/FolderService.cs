public class FolderService(IFolderRepository folderRepo) : IFolderService
{
    public async Task CreateFolderAsync(string folderName, string userId)
    {
        var folder = new Folder
        {
            Id = Guid.NewGuid(),
            FolderName = folderName,
            UserId = userId
        };

        await folderRepo.CreateForUserAsync(folder);
    }

    public async Task<IEnumerable<Folder?>> GetAllFoldersByUserAsync(string userId)
    {
        return await folderRepo.GetAllForUserAsync(userId);
    }

    public async Task<Folder?> DeleteByFolderNameAsync(string folderName)
    {
        return await folderRepo.DeleteByFolderNameAsync(folderName);
    }
}
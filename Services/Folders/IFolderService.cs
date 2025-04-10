public interface IFolderService
{
    Task CreateFolderAsync(string folderName, string userId);
    Task<IEnumerable<Folder?>> GetAllFoldersAsync(string userId);
    Task DeleteFolderAsync(string folderName, string userId);
}
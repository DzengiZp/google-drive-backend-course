public interface IFolderService
{
    Task CreateFolderAsync(string folderName, string userId);
    Task<IEnumerable<Folder?>> GetAllFoldersByUserAsync(string userId);
    Task<Folder?> DeleteByFolderNameAsync(string folderName);
}
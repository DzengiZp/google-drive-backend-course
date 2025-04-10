public interface IFolderRepository
{
    Task<Folder> CreateForUserAsync(Folder folder);
    Task<Guid> GetFolderIdByNameAsync(string folderName, string userId);
    Task<IEnumerable<Folder?>> GetAllForUserAsync(string userId);
    Task<Folder?> DeleteByFolderNameAsync(string folderName);
}
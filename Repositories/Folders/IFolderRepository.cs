public interface IFolderRepository
{
    Task<Folder> CreateForUserAsync(Folder folder);
    Task<IEnumerable<Folder?>> GetAllForUserAsync(string userId);
    Task<Folder?> DeleteByFolderNameAsync(string folderName);
}
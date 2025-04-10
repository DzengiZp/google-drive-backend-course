public interface IFolderRepository
{
    Task<Folder> CreateFolderInDbAsync(Folder folder);
    Task<Guid> GetFolderFromDbAsync(string folderName, string userId);
    Task<IEnumerable<Folder?>> GetAllFoldersFromDbAsync(string userId);
    Task DeleteFolderFromDbAsync(string folderName, string userId);
}
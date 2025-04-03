public interface IFolderService
{
    Task<Folder> CreateFolderAsync(FolderDto folderDto);
    Task<IEnumerable<Folder>> GetAllFoldersAsync();
    Task<Folder> GetFolderByIdAsync(int id);
    Task<Folder?> DeleteFolderByIdAsync(int id);
}
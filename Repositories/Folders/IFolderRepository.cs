public interface IFolderRepository
{
    Task<Folder> CreateAsync(Folder folder);
    Task<IEnumerable<Folder>> GetAllAsync();
    Task<Folder> GetByIdAsync(int id);
    Task<Folder> DeleteByIdAsync(int id);
}
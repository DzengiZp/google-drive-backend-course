public interface IFileRepository
{
    Task<File> UploadFileAsync(File file);
    Task<IEnumerable<File>> GetAllFilesAsync();
    Task<File?> GetFileByIdAsync(int id);
    Task<File?> DownloadFileByIdAsync(int id);
    Task<File?> DeleteFileByIdAsync(int id);
    Task<User?> CheckIfUserExistsAsync(Guid userId); //Put in user?
    Task<Folder?> CheckIfFolderExistsAsync(int folderId); //Put in folder?
}
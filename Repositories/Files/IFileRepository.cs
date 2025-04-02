public interface IFileRepository
{
    Task<File> UploadFileAsync(File file);
    Task<File?> GetFileByIdAsync(int id);
    Task<File?> DownloadFileByIdAsync(int id);
    Task<File?> DeleteFileByIdAsync(int id);
    Task<User?> CheckIfUserExistsAsync(Guid userId);
    Task<Folder?> CheckIfFolderExistsAsync(int folderId);
}
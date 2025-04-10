public interface IFileRepository
{
    Task<File> UploadFileAsync(File file);
    Task<IEnumerable<File>> GetAllFilesAsync(string userId);
    Task<File?> DownloadFileByNameAsync(string userId);
    Task DeleteFileByNameAsync(string userId);
}
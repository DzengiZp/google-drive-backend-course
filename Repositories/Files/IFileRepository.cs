public interface IFileRepository
{
    Task<File> UploadFileAsync(File file);
    Task<IEnumerable<File>> GetAllFilesAsync(string userId);
    Task<File?> DownloadFileByNameAsync(int id);
    Task<File?> DeleteFileByNameAsync(int id);
}
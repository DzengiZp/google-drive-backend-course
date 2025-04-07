public interface IFileService
{
    Task<File> UploadFileAsync(IFormFile uploadedFile, string userId, int folderId);
    Task<IEnumerable<File>> GetAllFilesAsync();
    Task<File?> GetFileByIdAsync(int id);
    Task<File?> DownloadFileByIdAsync(int id);
    Task<File?> DeleteFileByIdAsync(int id);
}
public interface IFileService
{
    Task<File> UploadFileAsync(IFormFile uploadedFile, string userId, Guid folderId);
    Task<File?> DownloadFileByNameAsync(int id);
    Task<IEnumerable<File>> GetAllFilesAsync();
    Task<File?> DeleteFileByNameAsync(int id);
}
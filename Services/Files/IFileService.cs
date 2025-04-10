public interface IFileService
{
    Task<File> UploadFileAsync(CreateNewFileDto uploadedFile, string userId);
    Task<IEnumerable<File>> GetAllFilesAsync(string userId);
    Task<File?> DownloadFileByNameAsync(string userId);
    Task DeleteFileByNameAsync(string fileName);
}
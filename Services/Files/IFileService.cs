public interface IFileService
{
    Task<File> UploadFileAsync(CreateNewFileDto uploadedFile, string userId);
    Task<IEnumerable<File>> GetAllFilesAsync(string userId);
    Task<File?> DownloadFileByNameAsync(string folderName, string userId, string fileName);
    Task DeleteFileByNameAsync(string folderName, string userId, string fileName);
}
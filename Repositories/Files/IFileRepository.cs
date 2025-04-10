public interface IFileRepository
{
    Task<File> UploadFileToDbAsync(File file);
    Task<IEnumerable<File>> GetAllFilesFromDbAsync(string userId);
    Task<File?> GetFileInFolderFromDbAsync(string folderName, string userId, string fileName);
    Task<(string, string)> DeleteFileFromDbAsync(string folderName, string userId, string fileName);
}
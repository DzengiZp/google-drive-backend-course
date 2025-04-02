public interface IFileService
{
    Task<File> UploadAsync(IFormFile uploadedFile, Guid userId, int folderI);
}
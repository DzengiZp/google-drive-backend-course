public class FileService(IFileRepository fileRepository) : IFileService
{
    public async Task<File> UploadFileAsync(IFormFile uploadedFile, string userId, string fileName)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            await uploadedFile.CopyToAsync(memoryStream);

            var fileContentBytes = memoryStream.ToArray();

            var file = new File
            {
                FileName = uploadedFile.FileName,
                FileExtension = Path.GetExtension(uploadedFile.FileName),
                FileContentBytes = fileContentBytes,
                UserId = userId,
                FolderId = Guid.NewGuid()
            };

            return await fileRepository.UploadFileAsync(file);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.StackTrace);
        }
    }

    public async Task<File?> DownloadFileByNameAsync(int id)
    {
        return await fileRepository.DownloadFileByNameAsync(id);
    }

    public async Task<IEnumerable<File>> GetAllFilesAsync()
    {
        return await fileRepository.GetAllFilesAsync();
    }

    public async Task<File?> DeleteFileByNameAsync(int id)
    {
        return await fileRepository.DeleteFileByNameAsync(id);
    }
}
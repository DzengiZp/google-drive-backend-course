public class FileService(IFileRepository fileRepository) : IFileService
{
    public async Task<File> UploadFileAsync(IFormFile uploadedFile, Guid userId, int folderId)
    {
        try
        {
            if (await fileRepository.CheckIfUserExistsAsync(userId) == null)
                throw new Exception("User doesn't exist");

            if (await fileRepository.CheckIfUserExistsAsync(userId) == null)
                throw new Exception("Folder doesn't exist");

            using var memoryStream = new MemoryStream();
            await uploadedFile.CopyToAsync(memoryStream);

            var fileContentBytes = memoryStream.ToArray();

            var file = new File
            {
                FileName = uploadedFile.FileName,
                FileExtension = Path.GetExtension(uploadedFile.FileName),
                FileContentBytes = fileContentBytes,
                UserId = userId,
                FolderId = folderId
            };

            return await fileRepository.UploadFileAsync(file);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<File?> DownloadFileByIdAsync(int id)
    {
        try
        {
            return fileRepository.DownloadFileByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<IEnumerable<File>> GetAllFilesAsync()
    {
        try
        {
            return fileRepository.GetAllFilesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<File?> GetFileByIdAsync(int id)
    {
        try
        {
            return fileRepository.GetFileByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<File?> DeleteFileByIdAsync(int id)
    {
        try
        {
            return fileRepository.DeleteFileByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
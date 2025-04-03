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

    public async Task<File?> DownloadFileByIdAsync(int id)
    {
        try
        {
            return await fileRepository.DownloadFileByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<File>> GetAllFilesAsync()
    {
        try
        {
            return await fileRepository.GetAllFilesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<File?> GetFileByIdAsync(int id)
    {
        try
        {
            return await fileRepository.GetFileByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<File?> DeleteFileByIdAsync(int id)
    {
        try
        {
            return await fileRepository.DeleteFileByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}
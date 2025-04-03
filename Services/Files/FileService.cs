public class FileService(IFileRepository fileRepository) : IFileService
{
    public async Task<File> UploadAsync(IFormFile uploadedFile, Guid userId, int folderId)
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
}
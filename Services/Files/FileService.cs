public class FileService(IFileRepository fileRepository) : IFileService
{
    public async Task<File> UploadAsync(IFormFile uploadedFile, Guid userId, int folderId)
    {
        var user = await fileRepository.CheckIfUserExistsAsync(userId);
        if (user is null) throw new Exception("User not found");

        var folder = await fileRepository.CheckIfFolderExistsAsync(folderId);
        if (folder is null) throw new Exception("Folder not found");

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
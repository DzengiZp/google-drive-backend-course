
using Microsoft.AspNetCore.Mvc;

public class FileService(IFileRepository _fileRepository) : IFileService
{
    public async Task<File> UploadAsync(IFormFile uploadedFile, Guid userId, int folderId)
    {
        var user = await _fileRepository.CheckIfUserExistsAsync(userId);
        if (user is null) throw new Exception("User not found");

        var folder = await _fileRepository.CheckIfFolderExistsAsync(folderId);
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

        return await _fileRepository.UploadFileAsync(file);
    }
}
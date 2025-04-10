public class FileService(IFileRepository fileRepository, IFolderRepository folderRepository) : IFileService
{
    public async Task<File> UploadFileAsync(CreateNewFileDto uploadedFile, string userId)
    {
        try
        {
            var folderId = await folderRepository.GetFolderIdByNameAsync(uploadedFile.FolderName, userId);

            using var memoryStream = new MemoryStream();

            if (uploadedFile.File is null) throw new Exception("No file was found to upload");

            await uploadedFile.File.CopyToAsync(memoryStream);

            var fileContentBytes = memoryStream.ToArray();

            var file = new File
            {
                FileName = uploadedFile.File.FileName,
                FileExtension = Path.GetExtension(uploadedFile.File.FileName),
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

    public async Task<IEnumerable<File>> GetAllFilesAsync(string userId)
    {
        return await fileRepository.GetAllFilesAsync(userId);
    }

    public async Task<File?> DownloadFileByNameAsync(string userId)
    {
        return await fileRepository.DownloadFileByNameAsync(userId);
    }

    public async Task DeleteFileByNameAsync(string userId)
    {
        await fileRepository.DeleteFileByNameAsync(userId);
    }
}
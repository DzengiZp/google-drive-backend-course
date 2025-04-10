using System.Data;

public class FileService(IFileRepository fileRepository, IFolderRepository folderRepository) : IFileService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uploadedFile"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NoNullAllowedException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<File> UploadFileAsync(CreateNewFileDto uploadedFile, string userId)
    {
        try
        {
            var folderId = await folderRepository.GetFolderFromDbAsync(uploadedFile.FolderName, userId);

            using var memoryStream = new MemoryStream();

            if (uploadedFile.File is null) throw new NoNullAllowedException("No file was found to upload");

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<File>> GetAllFilesAsync(string userId)
    {
        return await fileRepository.GetAllFilesAsync(userId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<File?> DownloadFileByNameAsync(string userId)
    {
        return await fileRepository.DownloadFileByNameAsync(userId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task DeleteFileByNameAsync(string userId)
    {
        await fileRepository.DeleteFileByNameAsync(userId);
    }
}
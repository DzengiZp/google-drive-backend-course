using System.Data;

public class FileService(IFileRepository fileRepository, IFolderRepository folderRepository) : IFileService
{
    /// <summary>
    /// Uploads a file by converting it to a byte array and saving it to the database, associating it with a folder and user.
    /// </summary>
    /// <param name="uploadedFile">Specifies the file data being uploaded to the database.</param>
    /// <param name="userId">Specifies the ID of the user uploading the file.</param>
    /// <returns>The file object after being saved to the database.</returns>
    /// <exception cref="NoNullAllowedException">Thrown when no file is provided or folder name is null.</exception>
    /// <exception cref="Exception">Thrown if an error occurs during the upload process.</exception>
    public async Task<File> UploadFileAsync(CreateNewFileDto uploadedFile, string userId)
    {
        try
        {
            var folderId = await folderRepository.GetFolderFromDbAsync(uploadedFile.FolderName, userId);

            if (folderId == Guid.Empty) throw new DirectoryNotFoundException("Folder does not exist to download files from");

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

            return await fileRepository.UploadFileToDbAsync(file);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves all files that belong to the specified user.
    /// </summary>
    /// <param name="userId">The ID of the user who owns the files.</param>
    /// <returns>A list of the files from the database owned by the user.</returns>
    public async Task<IEnumerable<File>> GetAllFilesAsync(string userId)
    {
        return await fileRepository.GetAllFilesFromDbAsync(userId);
    }

    /// <summary>
    /// Retrieves a specific file from a specific folder for the specified user.
    /// </summary>
    /// <param name="folderName">The name of the folder that contains the file.</param>
    /// <param name="userId">The ID of the user who owns the folder and file.</param>
    /// <param name="fileName">The name of the file to download.</param>
    /// <returns>The file found from the database.</returns>
    public async Task<File?> DownloadFileByNameAsync(string folderName, string userId, string fileName)
    {
        return await fileRepository.GetFileInFolderFromDbAsync(folderName, userId, fileName);
    }

    /// <summary>
    /// Deletes all files associated with the specified user from the database.
    /// </summary>
    /// <param name="userId">The ID of the user who owns the file to be deleted.</param>
    public async Task DeleteFileByNameAsync(string folderName, string userId, string fileName)
    {
        await fileRepository.DeleteFileFromDbAsync(folderName, userId, fileName);
    }
}
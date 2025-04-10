using Microsoft.EntityFrameworkCore;

public class FileRepository(ApplicationDbContext context, IFolderRepository folderRepository) : IFileRepository
{
    /// <summary>
    /// Adds and saves an uploaded file to the database.
    /// </summary>
    /// <param name="file">Recieves the file object to be uploaded.</param>
    /// <returns>The uploaded file object.</returns>
    public async Task<File> UploadFileToDbAsync(File file)
    {
        await context.Files.AddAsync(file);
        await context.SaveChangesAsync();

        return file;
    }

    /// <summary>
    /// Retrieves all the files from the database for the specified user based on its ID.
    /// </summary>
    /// <param name="userId">Specifies the ID of the user who owns all the files.</param>
    /// <returns>A list of all the files found in the database or an empty list if no files are found./returns>
    public async Task<IEnumerable<File>> GetAllFilesFromDbAsync(string userId)
    {
        return await context.Files.Where(file => file.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// Retrieves a file from the database in the specified folder for the user based on the users ID.
    /// </summary>
    /// <param name="folderName">Specifies the folder all the retrieved files lies within.</param>
    /// <param name="userId">Specifies the ID of the user who owns all the files in the given folder.</param>
    /// <returns>The retrieved file from the database.</returns>
    public async Task<File?> GetFileInFolderFromDbAsync(string folderName, string userId, string fileName)
    {
        var folderId = await folderRepository.GetFolderFromDbAsync(folderName, userId);

        var file = await context.Files.Where(file => file.UserId == userId && file.FolderId == folderId && file.FileName == fileName)
        .FirstOrDefaultAsync() ?? throw new FileNotFoundException($"File does not exist in the folder ${folderName}");

        return file;
    }

    /// <summary>
    /// Deletes a file from the database that belongs to the user based on the users ID.
    /// </summary>
    /// <param name="userId">Specifies the ID of the user who owns the file.</param>
    public async Task<(string, string)> DeleteFileFromDbAsync(string folderName, string userId, string fileName)
    {
        var folderId = await folderRepository.GetFolderFromDbAsync(folderName, userId);

        var rowsDeleted = await context.Files.Where(file => file.UserId == userId && file.FolderId == folderId && file.FileName == fileName).ExecuteDeleteAsync();

        if (rowsDeleted == 0) throw new FileNotFoundException("File not found in database");

        return (folderName, fileName);
    }
}
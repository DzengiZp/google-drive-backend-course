public class FolderService(IFolderRepository folderRepo) : IFolderService
{
    public async Task<Folder> CreateFolderAsync(FolderDto folderDto)
    {
        try
        {
            var existingFolder = await folderRepo.GetAllAsync();

            if (existingFolder.Any(f => f.FolderName == folderDto.FolderName && f.UserId == folderDto.UserId))
                throw new Exception("Folder name for user already exists");

            var folder = new Folder
            {
                FolderName = folderDto.FolderName,
                UserId = folderDto.UserId
            };

            return await folderRepo.CreateAsync(folder);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Folder?> DeleteFolderByIdAsync(int id)
    {
        try
        {
            return await folderRepo.DeleteByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<Folder>> GetAllFoldersAsync()
    {
        try
        {
            return await folderRepo.GetAllAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Folder> GetFolderByIdAsync(int id)
    {
        try
        {
            return await folderRepo.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
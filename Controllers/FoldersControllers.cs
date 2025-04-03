using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// [Authorize]
[Route("api/folders")]
[ApiController]
public class FoldersControllers(IFolderService folderService) : ControllerBase
{
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult> Create([FromBody] FolderDto folderDto)
    {
        await folderService.CreateFolderAsync(folderDto);
        return Ok(new Folder { FolderName = folderDto.FolderName, UserId = folderDto.UserId });
    }

    [HttpGet]
    [Route("get{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var folder = await folderService.GetFolderByIdAsync(id);
        if (folder == null) return NotFound("Folder doesn't exist");

        return Ok(folder);
    }

    [HttpGet]
    [Route("getall")]
    public async Task<ActionResult> GetAll()
    {
        var folders = await folderService.GetAllFoldersAsync();
        if (folders == null) return NotFound("There are no folders");

        return Ok(folders);
    }

    [HttpDelete]
    [Route("delete{folderId}")]
    public async Task<ActionResult> DeleteById(int folderId)
    {
        var folder = await folderService.DeleteFolderByIdAsync(folderId);
        if (folder == null) return NotFound("Folder doesn't exist");

        return NoContent();
    }
}
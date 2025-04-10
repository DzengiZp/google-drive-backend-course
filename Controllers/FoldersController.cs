using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/folders")]
public class FoldersController(IFolderService folderService) : ControllerBase
{

    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] FolderDto folderDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You need to login to create a folder");

            await folderService.CreateFolderAsync(folderDto.FolderName, userId);

            return Ok($"Folder \"{folderDto.FolderName}\" created");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("get-all")]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You need to login to retrieve your folders");

            var folders = await folderService.GetAllFoldersByUserAsync(userId);

            return Ok(folders);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete{folderName}")]
    public async Task<ActionResult> DeleteById(string folderName)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You need to login to delete a folder");

            var folder = await folderService.DeleteByFolderNameAsync(folderName);

            if (folder == null) return NotFound("Folder doesn't exist");

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
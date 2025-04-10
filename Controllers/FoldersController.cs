using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/folders")]
public class FoldersController(IFolderService folderService) : ControllerBase
{
    /// <summary>
    /// Creates a new folder for the authenticated user.
    /// </summary>
    /// <param name="folderDto">Specifies the folder data sent from the client.</param>
    /// <returns>An error response if user is not logged in or if the folder wasn't created. A success message if the folder has been created.</returns>
    [HttpPost("create")]
    public async Task<ActionResult> CreateFolder([FromBody] FolderDto folderDto)
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

    /// <summary>
    /// Gets all folders for the authenticated user.
    /// </summary>
    /// <returns>An error response if user is not logged in or if the folder wasn't retrieved. A success message if the folder has been retrieved.</returns>
    [HttpGet("get-all")]
    public async Task<ActionResult> GetAllFolders()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You need to login to retrieve your folders");

            var folders = await folderService.GetAllFoldersAsync(userId);

            return Ok(folders);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes the specified folder for the authenticated user.
    /// </summary>
    /// <param name="folderName">Specifies the name of the folder to be deleted.</param>
    /// <returns>An error response if user is not logged in or if the folder wasn't deleted. A success message if the folder has been retrieved</returns>
    [HttpDelete("delete{folderName}")]
    public async Task<ActionResult> DeleteFolder(string folderName)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You need to login to delete a folder");

            await folderService.DeleteFolderAsync(folderName, userId);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
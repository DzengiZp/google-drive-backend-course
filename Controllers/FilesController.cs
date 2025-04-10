using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/files")]
public class FilesController(IFileService fileService) : ControllerBase
{
    /// <summary>
    /// Uploads a file for the authenticated user, to a specified folder.
    /// </summary>
    /// <param name="uploadedFile">The file data including folder name.</param>
    /// <returns>Returns a success message if file was uploaded, or an error response .</returns>
    [HttpPost("upload")]
    public async Task<ActionResult> UploadFile([FromForm] CreateNewFileDto uploadedFile)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You have to login to upload files");

            var file = await fileService.UploadFileAsync(uploadedFile, userId);

            if (file == null) return BadRequest("Error when uploading file");

            return Ok("Uploaded");
        }
        catch (Exception ex)
        {
            return StatusCode(405, ex.Message);
        }
    }

    /// <summary>
    /// Retreives all the files that belong to the authenticated user.
    /// </summary>
    /// <returns>A list of the user's files if successful, a not found response if no files are found or a error something went wrong in the process of retrieving the files.</returns>
    [HttpGet("getall")]
    public async Task<ActionResult> GetAllFiles()
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You have to login to see files");

            var files = await fileService.GetAllFilesAsync(userId);

            if (files == null) return NotFound("There are no files");

            return Ok(files);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Downloads a specific file from a specified folder for the authenticated user.
    /// </summary>
    /// <param name="folderName">The name of the folder where the file is stored.</param>
    /// <param name="fileName">The name of the file to download.</param>
    /// <returns>The file content as a download, a not found error if file does not exist or a error message if something went wrong with uploading process.</returns>
    [HttpGet("download/{fileName}")]
    public async Task<ActionResult> DownloadFile([FromQuery] string folderName, string fileName)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You have to login to download the file");

            var file = await fileService.DownloadFileByNameAsync(folderName, userId, fileName);

            if (file == null) return NotFound($"File does not exist");

            return File(file.FileContentBytes, "application/octet-stream", file.FileName, enableRangeProcessing: true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Deletes the specified file name associated with the authenticated user.
    /// </summary>
    /// <param name="fileName">Specifies the file name for the file to be deleted.</param>
    /// <returns></returns>
    [HttpDelete("delete")]
    public async Task<ActionResult> DeleteFile([FromQuery] string folderName, [FromQuery] string fileName)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You have to login to download the file");

            await fileService.DeleteFileByNameAsync(folderName, userId, fileName);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/files")]
public class FilesController(IFileService fileService) : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uploadedFile"></param>
    /// <returns></returns>
    [HttpPost("upload")]
    public async Task<ActionResult> UploadFile([FromForm] CreateNewFileDto uploadedFile)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You have to login to upload files");

        var file = await fileService.UploadFileAsync(uploadedFile, userId);

        if (file == null) return BadRequest("Error when uploading file");

        return Ok("Uploaded");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    [HttpGet("download/{fileName}")]
    public async Task<ActionResult> DownloadFile(string fileName)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId)) return Unauthorized("You have to login to download the file");

            var file = await fileService.DownloadFileByNameAsync(userId);

            if (file is null) return NotFound($"File does not exist");

            return File(file.FileContentBytes, "application/octet-stream", file.FileName);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    [HttpDelete("delete/{fileName}")]
    public async Task<ActionResult> DeleteFile(string fileName)
    {
        await fileService.DeleteFileByNameAsync(fileName);

        return NoContent();
    }
}
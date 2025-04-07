using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/files")]
public class FilesController(IFileService fileService) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<ActionResult> UploadFile(IFormFile uploadedFile, [FromQuery] int folderId)
    {
        // var file = await fileService.UploadFileAsync(uploadedFile, userId, folderId);

        // if (file == null) return BadRequest("Can't upload null, select a file");

        return Ok();
    }

    [HttpGet("getall")]
    public async Task<ActionResult> GetAllFiles()
    {
        var files = await fileService.GetAllFilesAsync();

        if (files == null) return NotFound("There are no files");

        return Ok(files);
    }

    [HttpGet("getFile/{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var file = await fileService.GetFileByIdAsync(id);

        if (file == null) return NotFound("File does not exist");

        return Ok(new
        {
            file.Id,
            file.FileName,
            file.FileContentBytes,
            file.FileExtension,
            file.FolderId,
            file.UserId
        });
    }

    [HttpGet("download/{id}")]
    public async Task<ActionResult> DownloadById(int id)
    {
        var file = await fileService.GetFileByIdAsync(id);

        if (file is null) return NotFound($"File does not exist");

        return File(file.FileContentBytes, "application/octet-stream", file.FileName);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteById(int id)
    {
        var file = await fileService.DeleteFileByIdAsync(id);
        if (file == null) return NotFound(file);

        return NoContent();
    }
}
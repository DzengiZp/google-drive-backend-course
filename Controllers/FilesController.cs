using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/files")]
[ApiController]
public class FilesControllers(ApplicationDbContext _context, IFileRepository _fileRepo, IFileService _fileService) : ControllerBase
{

    [HttpPost]
    [Route("upload")]
    public async Task<ActionResult> Upload(IFormFile uploadedFile, [FromQuery] Guid userId, int folderId)
    {
        var file = await _fileService.UploadAsync(uploadedFile, userId, folderId);

        if (file == null) return BadRequest("Can't upload null, select a file");

        return Ok(file);
    }

    /// <summary>
    /// Testing purposes because some files have too large bytes, so used for validating data in Scalar instead of Database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("getFile/{id}")]
    public async Task<ActionResult> Get(int id)
    {
        var file = await _context.Files.FindAsync(id);

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

    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var file = await _fileRepo.DeleteFileByIdAsync(id);
        if (file == null) return NotFound(file);

        return NoContent();
    }

    [HttpGet]
    [Route("download/{id}")]
    public async Task<ActionResult> Download(int id)
    {
        var file = await _fileRepo.GetFileByIdAsync(id);
        if (file is null) return NotFound(file);

        return File(file.FileContentBytes, "application/octet-stream", file.FileName);
    }
}
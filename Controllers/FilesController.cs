using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/files")]
[ApiController]
public class FilesControllers(ApplicationDbContext context, IFileRepository fileRepo, IFileService fileService) : ControllerBase
{
    [HttpPost]
    [Route("upload")]
    public async Task<ActionResult> UploadFile(IFormFile uploadedFile, [FromQuery] int folderId)
    {
        try
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdString, out Guid userId)) return Unauthorized("Invalid user ID in token.");
            var file = await fileService.UploadAsync(uploadedFile, userId, folderId);

            if (file == null) return BadRequest("Can't upload null, select a file");

            return Ok(file);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    // Testing purposes because some files have too large bytes, so used for validating data in Scalar instead of Database.
    [HttpGet] //NO AUTH ON THIS METHOD.
    [Route("getFile/{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        //Create for Repo to handle instead.
        var file = await context.Files.FindAsync(id);

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
    public async Task<ActionResult> DeleteById(int id)
    {
        var file = await fileRepo.DeleteFileByIdAsync(id);

        if (file == null) return NotFound(file);

        return NoContent();
    }

    [HttpGet]
    [Route("download/{id}")]
    public async Task<ActionResult> DownloadById(int id)
    {
        var file = await fileRepo.GetFileByIdAsync(id);
        if (file is null) return NotFound(file);

        return File(file.FileContentBytes, "application/octet-stream", file.FileName);
    }
}
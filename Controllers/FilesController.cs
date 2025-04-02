using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/files")]
[ApiController]
public class FilesControllers(ApplicationDbContext context, IFileRepository fileRepo, IFileService fileService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    [Route("upload")]
    public async Task<ActionResult> Upload(IFormFile uploadedFile, [FromQuery] int folderId)
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


    // Testing purposes because some files have too large bytes, so used for validating data in Scalar instead of Database.
    [HttpGet] //NO AUTH ON THIS METHOD.
    [Route("getFile/{id}")]
    public async Task<ActionResult> Get(int id)
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

    [Authorize]
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var file = await fileRepo.DeleteFileByIdAsync(id);
        if (file == null) return NotFound(file);

        return NoContent();
    }

    [Authorize]
    [HttpGet]
    [Route("download/{id}")]
    public async Task<ActionResult> Download(int id)
    {
        var file = await fileRepo.GetFileByIdAsync(id);
        if (file is null) return NotFound(file);

        return File(file.FileContentBytes, "application/octet-stream", file.FileName);
    }
}
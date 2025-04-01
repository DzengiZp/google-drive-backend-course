using Microsoft.AspNetCore.Mvc;

[Route("api/files")]
[ApiController]
public class FilesControllers : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public FilesControllers(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("upload")]
    public ActionResult UploadFile(IFormFile uploadedFile, Guid userId, int folderId)
    {
        var existingUser = _context.Users.Find(userId);
        if (existingUser == null) return NotFound("User does not exist");

        var existingFolder = _context.Folders.Find(folderId);
        if (existingFolder == null) return NotFound("Folder does not exist");

        string path = Path.Combine(Directory.GetCurrentDirectory(), "google-drive-backend");
        if (path is null) return BadRequest();

        using var memoryStream = new MemoryStream();
        uploadedFile.CopyTo(memoryStream);

        var fileContentBytes = memoryStream.ToArray();

        var file = new File
        {
            FileName = uploadedFile.FileName,
            FileExtension = Path.GetExtension(uploadedFile.FileName),
            FileContentBytes = fileContentBytes,
            UserId = userId,
            FolderId = folderId
        };

        _context.Files.Add(file);
        _context.SaveChanges();

        return Ok(new { file.Id });
    }

    /// <summary>
    /// Testing purposes because some files have too large bytes, so used for validating data in Scalar instead of Database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("getFile/{id}")]
    public ActionResult GetFile(int id)
    {
        var file = _context.Files.Find(id);
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
    public ActionResult DeleteFile(int id)
    {
        var file = _context.Files.FirstOrDefault(f => f.Id == id);
        if (file is null) return NotFound("File doesn't exist.");

        _context.Files.Remove(file);
        _context.SaveChanges();

        return NoContent();
    }

    [HttpGet]
    [Route("download/{id}")]
    public ActionResult DownloadFile(int id)
    {
        var file = _context.Files.Find(id);
        if (file is null) return NotFound("File does not exist");

        return File(file.FileContentBytes, "application/octet-stream", file.FileName);
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Text;


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
    [Route("create")]
    public ActionResult CreateFile([FromBody] CreateFileDto createFileDto)
    {
        var existingUser = _context.Users.Find(createFileDto.UserId);
        if (existingUser is null) return NotFound("User does not exist");

        var existingFolder = _context.Folders.Find(createFileDto.FolderId);
        if (existingFolder is null) return NotFound("Folder does not exist");

        var file = new File
        {
            FileName = createFileDto.FileName,
            FileExtension = createFileDto.FileExtension,
            UserId = createFileDto.UserId,
            FolderId = createFileDto.FolderId
        };

        _context.Files.Add(file);
        _context.SaveChanges();

        return Ok(file);
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

        return File(file.FileContent, "application/octet-stream", file.FileName + file.FileExtension);
    }
}
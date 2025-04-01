using Microsoft.AspNetCore.Mvc;
using System.IO;

[Route("api/folders")]
[ApiController]
public class FoldersControllers : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public FoldersControllers(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("create")]
    public ActionResult CreateFolder([FromBody] FolderDto folderDto)
    {
        var existingUser = _context.Users.Find(folderDto.UserId);
        if (existingUser == null) return NotFound("User not found");

        var folder = new Folder
        {
            FolderName = folderDto.FolderName,
            UserId = folderDto.UserId
        };

        _context.Folders.Add(folder);
        _context.SaveChanges();

        return Ok(folder);
    }

}
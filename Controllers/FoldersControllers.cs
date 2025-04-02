using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.IO.Compression;

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
    public async Task<ActionResult> CreateFolder([FromBody] FolderDto folderDto)
    {
        var existingUser = await _context.Users.FindAsync(folderDto.UserId);
        if (existingUser == null) return NotFound("User not found");

        var folder = new Folder
        {
            FolderName = folderDto.FolderName,
            UserId = folderDto.UserId
        };

        await _context.Folders.AddAsync(folder);
        await _context.SaveChangesAsync();

        return Ok(folder);
    }

    [HttpDelete]
    [Route("delete{folderId}")]
    public async Task<ActionResult> DeleteFolder(int folderId)
    {
        var existingFolder = await _context.Folders.FindAsync(folderId);
        if (existingFolder == null) return NotFound("Folder not found");

        _context.Folders.Remove(existingFolder);
        await _context.SaveChangesAsync();
        // Console.WriteLine(whatever);

        return NoContent();
    }
}
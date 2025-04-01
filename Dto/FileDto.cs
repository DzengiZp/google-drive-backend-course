using System.ComponentModel.DataAnnotations;

public class FileDto
{
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public byte[] FileContentBytes { get; set; } = [];
    public Guid UserId { get; set; }
    public int FolderId { get; set; }
}
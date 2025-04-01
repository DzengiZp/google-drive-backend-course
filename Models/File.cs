using System.ComponentModel.DataAnnotations;

public class File
{
    public int Id { get; set; }
    [Required]
    public string FileName { get; set; } = string.Empty;
    [Required]
    public string FileExtension { get; set; } = string.Empty;
    [Required]
    public required byte[] FileContentBytes { get; set; }
    [Required]
    public Guid UserId { get; set; }
}
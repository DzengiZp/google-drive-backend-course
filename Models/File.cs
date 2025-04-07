using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class File
{
    public Guid Id { get; set; }
    [Required]
    public string FileName { get; set; } = string.Empty;
    [Required]
    public string FileExtension { get; set; } = string.Empty;
    [Required]
    public byte[] FileContentBytes { get; set; } = [];
    [Required]
    public string UserId { get; set; } = string.Empty;
    public Guid FolderId { get; set; }
    [JsonIgnore]
    public Folder? Folder { get; set; }
}
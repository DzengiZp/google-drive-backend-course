using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class File
{
    public int Id { get; set; }
    [Required]
    public string FileName { get; set; } = string.Empty;
    [Required]
    public string FileExtension { get; set; } = string.Empty;
    [Required]
    public string FileContent { get; set; } = string.Empty;
    [Required]
    public Guid UserId { get; set; }
    public int FolderId { get; set; }
    [JsonIgnore]
    public Folder? Folder { get; set; }
}
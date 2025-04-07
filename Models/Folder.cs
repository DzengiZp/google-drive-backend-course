using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Folder
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(15, ErrorMessage = "Max 15 characters allowed.")]
    public string FolderName { get; set; } = string.Empty;
    [JsonIgnore]
    public List<File> File { get; set; } = [];
    public string UserId { get; set; } = string.Empty;
}
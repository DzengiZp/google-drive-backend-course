using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Folder
{
    public int Id { get; set; }
    [Required]
    public string FolderName { get; set; } = string.Empty;
    [JsonIgnore]
    public List<File> File { get; set; } = [];
    public Guid UserId { get; set; }
}
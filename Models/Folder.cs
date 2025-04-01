using System.ComponentModel.DataAnnotations;

public class Folder
{
    public int Id { get; set; }
    [Required]
    public string FolderName { get; set; } = string.Empty;
    public List<File> File { get; set; } = [];
    public Guid UserId { get; set; }
}
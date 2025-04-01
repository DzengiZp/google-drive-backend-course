public class CreateFileDto
{
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public int FolderId { get; set; }
}
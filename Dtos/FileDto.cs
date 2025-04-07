public class FileDto
{
    public string FileName { get; set; } = string.Empty;
    public string FileExtension { get; set; } = string.Empty;
    public byte[] FileContentBytes { get; set; } = [];
    public string UserId { get; set; } = string.Empty;
    public int FolderId { get; set; }
}
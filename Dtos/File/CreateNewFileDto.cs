public class CreateNewFileDto
{
    public IFormFile? File { get; set; }
    public string FolderName { get; set; } = string.Empty;
}
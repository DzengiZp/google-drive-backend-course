using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<File> Files { get; set; }
    public DbSet<Folder> Folders { get; set; }

    /// <summary>
    /// Configures unique constraints for both file and folders relationships. Ensures that no duplicate value for folder name and file name can exist for each user.
    /// </summary>
    /// <param name="modelBuilder">The necessary builder required to construct the model for Entity Framework</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Folder>()
        .HasIndex(f => new { f.UserId, f.FolderName })
        .IsUnique();

        modelBuilder.Entity<File>()
        .HasIndex(f => new { f.FileName, f.FolderId })
        .IsUnique();
    }
}
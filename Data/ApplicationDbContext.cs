using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<File> Files { get; set; }
    public DbSet<Folder> Folders { get; set; }

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
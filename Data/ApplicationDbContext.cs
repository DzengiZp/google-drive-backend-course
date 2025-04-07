using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{
    // public DbSet<User> Users { get; set; }
    public DbSet<File> Files { get; set; }
    public DbSet<Folder> Folders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<File>()
        .HasIndex(x => new { x.FolderId })
        .IsUnique();

        modelBuilder.Entity<Folder>()
        .HasIndex(x => new { x.UserId })
        .IsUnique();
    }
}
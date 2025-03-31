using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    DbSet<User> Users { get; set; }
}
using Microsoft.EntityFrameworkCore;

namespace Swap.API.Database;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Swap);

        modelBuilder.Entity<Users>()
            .HasIndex(u => u.UserName)
            .IsUnique();
    }
}

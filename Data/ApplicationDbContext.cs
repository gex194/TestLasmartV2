using Microsoft.EntityFrameworkCore;
using TestLasmartV2.Entities;

namespace TestLasmartV2.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<Point> Points { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Describing relations
        modelBuilder.Entity<Point>()
            .HasMany(p => p.Comments)
            .WithOne()
            .HasForeignKey(c => c.PointId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
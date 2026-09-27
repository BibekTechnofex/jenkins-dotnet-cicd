using Microsoft.EntityFrameworkCore;
using TransferMock.Data.Entities;

namespace TransferMock.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Entities.User> Users => Set<Entities.User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Entities.User>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(e => new { e.UserId, e.UserType })
                .IsUnique();
        });
    }
}

using Microsoft.EntityFrameworkCore;
using MT.Domain.Entity;

namespace MT.Infrastructure.Data.Context;

public class TrackingContext : DbContext
{

    public TrackingContext(DbContextOptions<TrackingContext> options)
        : base(options)
    {
    }

    public TrackingContext() 
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres;");
        }
    }
    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userclaim> Userclaims { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Items).HasConstraintName("FK_Items_Users_UserId");
            entity.Property(e => e.ItemId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.TagId).ValueGeneratedOnAdd();
        });
    }
}

using Entities.Models;
using Repository.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new MaterialTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MaterialConfiguration());
        modelBuilder.ApplyConfiguration(new StorageUnitConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeStorageUnitConfiguration());
        modelBuilder.ApplyConfiguration(new DestinationConfiguration());        
    }

    public DbSet<Recipe>? Recipes { get; set; }
    public DbSet<Material>? Materials { get; set; }
    public DbSet<MaterialType>? MaterialTypes { get; set; }
    public DbSet<StorageUnit>? StorageUnits { get; set; }
    public DbSet<RecipeStorageUnit>? RecipeStorageUnits { get; set; }
    public DbSet<Job>? Jobs { get; set; }
    public DbSet<Destination>? Destinations { get; set; }
}

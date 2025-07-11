using BiogenomApi.Domain.Entities;
using BiogenomApi.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BiogenomApi.Infrastructure;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Vitamin> Vitamins { get; set; }
    public DbSet<DietarySupplement> DietarySupplements { get; set; }
    public DbSet<DietarySupplementImage> DietarySupplementImages { get; set; }
    public DbSet<VitaminDietarySupplement> VitaminDietarySupplements { get; set; }
    public DbSet<Food> Foods { get; set; }

    public DataContext()
    {
    }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}
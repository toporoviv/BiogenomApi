using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class DietarySupplementConfiguration : IEntityTypeConfiguration<DietarySupplement>
{
    public void Configure(EntityTypeBuilder<DietarySupplement> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Title).IsRequired().HasMaxLength(100);
        builder.Property(prop => prop.Description).IsRequired().HasMaxLength(1000);
        builder.Property(prop => prop.Application).IsRequired().HasMaxLength(1000);
            
        builder
            .HasMany(ds => ds.Images)
            .WithOne(img => img.DietarySupplement)
            .HasForeignKey(img => img.DietarySupplementId);
    }
}
using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class DietarySupplementImageConfiguration : IEntityTypeConfiguration<DietarySupplementImage>
{
    public void Configure(EntityTypeBuilder<DietarySupplementImage> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Data).IsRequired();
            
        builder.HasIndex(prop => prop.DietarySupplementId)
            .HasDatabaseName("IX_DietarySupplementImages_DietarySupplementId");
    }
}
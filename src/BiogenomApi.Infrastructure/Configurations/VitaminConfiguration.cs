using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class VitaminConfiguration : IEntityTypeConfiguration<Vitamin>
{
    public void Configure(EntityTypeBuilder<Vitamin> builder)
    {
        builder.HasKey(prop => prop.Id);
        builder.Property(prop => prop.Title).IsRequired().HasMaxLength(100);
        builder.Property(prop => prop.MeasurementUnit).IsRequired();
        builder.Property(prop => prop.ImportanceForHealth).IsRequired().HasMaxLength(1000);
        builder.Property(prop => prop.Prevention).HasMaxLength(1000);
        builder.Property(prop => prop.ScarcityManifestation).HasMaxLength(1000);
    }
}
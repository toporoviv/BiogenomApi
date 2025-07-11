using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class VitaminDietarySupplementConfiguration : IEntityTypeConfiguration<VitaminDietarySupplement>
{
    public void Configure(EntityTypeBuilder<VitaminDietarySupplement> builder)
    {
        builder
            .HasKey(vds => new { vds.VitaminId, vds.DietarySupplementId });
        
        builder
            .HasOne(vds => vds.Vitamin)
            .WithMany(v => v.RelatedSupplements)
            .HasForeignKey(vds => vds.VitaminId);

        builder
            .HasOne(vds => vds.DietarySupplement)
            .WithMany(ds => ds.RelatedVitamins)
            .HasForeignKey(vds => vds.DietarySupplementId);
            
        builder.OwnsOne(x => x.Amount, a =>
        {
            a.Property(p => p.Value).HasColumnName("Amount");
            a.Property(p => p.Unit).HasColumnName("AmountUnit");
        });
    }
}
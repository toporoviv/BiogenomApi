using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class FoodVitaminConfiguration : IEntityTypeConfiguration<FoodVitamin>
{
    public void Configure(EntityTypeBuilder<FoodVitamin> builder)
    {
        builder.HasKey(x => new { x.FoodId, x.VitaminId });

        builder.HasOne(x => x.Food)
            .WithMany(x => x.Vitamins)
            .HasForeignKey(x => x.FoodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Vitamin)
            .WithMany()
            .HasForeignKey(x => x.VitaminId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Amount, a =>
        {
            a.Property(p => p.Value).HasColumnName("Amount");
            a.Property(p => p.Unit).HasColumnName("AmountUnit");
        });
    }
}
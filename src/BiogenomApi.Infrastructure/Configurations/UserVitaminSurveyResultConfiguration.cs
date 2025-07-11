using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class UserVitaminSurveyResultConfiguration : IEntityTypeConfiguration<UserVitaminSurveyResult>
{
    public void Configure(EntityTypeBuilder<UserVitaminSurveyResult> builder)
    {
        builder.HasKey(r => new { r.UserVitaminSurveyId, r.VitaminId });

        builder.HasOne(r => r.UserVitaminSurvey)
            .WithMany(s => s.Results)
            .HasForeignKey(r => r.UserVitaminSurveyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne(x => x.Amount, a =>
        {
            a.Property(p => p.Value).HasColumnName("Amount");
            a.Property(p => p.Unit).HasColumnName("AmountUnit");
        });
            
        builder.HasOne(r => r.Vitamin)
            .WithMany()
            .HasForeignKey(r => r.VitaminId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class UserVitaminSurveyConfiguration : IEntityTypeConfiguration<UserVitaminSurvey>
{
    public void Configure(EntityTypeBuilder<UserVitaminSurvey> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.SurveyAtUtc)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.HasOne(s => s.User)
            .WithMany(u => u.VitaminSurveys)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
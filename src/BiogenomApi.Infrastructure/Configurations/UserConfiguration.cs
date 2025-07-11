using BiogenomApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiogenomApi.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.Property(user => user.Email).IsRequired();
        builder.Property(user => user.Birthday).IsRequired();
        builder.Property(user => user.Gender).IsRequired();
        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(100);

        builder.HasIndex(user => user.Gender)
            .HasDatabaseName("IX_Users_Gender");

        builder.HasIndex(user => user.Email)
            .IsUnique()
            .HasDatabaseName("IX_Users_Email");
    }
}
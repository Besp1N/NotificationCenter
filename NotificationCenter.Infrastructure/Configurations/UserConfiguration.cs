using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationCenter.Domain.Entities;

namespace NotificationCenter.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
        
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
        
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        
        builder.Property(u => u.CreatedAtUtc)
            .IsRequired()
            .HasColumnType("timestamptz");

        builder.Property(u => u.UpdatedAtUtc)
            .HasColumnType("timestamptz");
        
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
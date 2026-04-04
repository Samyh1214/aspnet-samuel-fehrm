using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymPortal.Infrastructure.Data.Configurations;

public class GymClassConfiguration : IEntityTypeConfiguration<GymClass>
{
    public void Configure(EntityTypeBuilder<GymClass> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Instructor)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(x => x.Bookings)
            .WithOne(x => x.GymClass)
            .HasForeignKey(x => x.GymClassId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
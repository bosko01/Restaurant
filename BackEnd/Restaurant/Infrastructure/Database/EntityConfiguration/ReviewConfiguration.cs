using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfiguration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(x => x.Text)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(x => x.Grade)
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(review => review.User)
               .WithOne()
               .HasForeignKey<Review>(reservation => reservation.UserId);

            builder.HasOne(r => r.Restaurant)
             .WithMany(rest => rest.Reviews)
             .HasForeignKey(r => r.RestaurantId);
        }
    }
}
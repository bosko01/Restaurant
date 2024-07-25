using Domain.Enums.Restaurant;
using Domain.Models;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfiguration
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.ToTable("Restaurants");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(x => x.Location)
                .HasColumnType("nvarchar(255)");

            builder.Property(x => x.Email)
                .HasConversion(new EmailConverter())
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasConversion(new PhoneConverter())
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.Property(x => x.MenuUrl)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(x => x.WorkingHoursFrom)
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.WorkingHoursTo)
                .HasColumnType("time")
                .IsRequired();

            builder.Property(r => r.Category)
                 .HasConversion(new EnumListToStringConverter<ERestaurantCategories>())
                 .HasColumnName("Category")
                 .IsRequired();

            builder.HasMany(r => r.Reviews)
            .WithOne(re => re.Restaurant)
            .HasForeignKey(re => re.RestaurantId);

            builder.HasMany(r => r.Tables)
            .WithOne(t => t.Restaurant)
            .HasForeignKey(t => t.RestaurantId);
        }
    }
}
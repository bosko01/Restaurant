using Domain.Models;
using Infrastructure.Database.Enumerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfiguration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TableId)
                .IsRequired();

            builder.Property(x => x.NumberOfPeople)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnType("decimal(8,2)")
                .IsRequired();

            builder.ConfigureEnum(x => x.Status);

            builder.HasOne(r => r.User)
             .WithMany()
             .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Restaurant)
                .WithMany()
                .HasForeignKey(r => r.RestaurantId);

            builder.HasOne(r => r.Table)
                .WithMany()
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
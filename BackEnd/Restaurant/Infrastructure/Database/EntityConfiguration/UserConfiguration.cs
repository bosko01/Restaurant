using Domain.Models;
using Infrastructure.Database.Converters;
using Infrastructure.Database.Enumerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnType("nvarchar(100)");

            builder.ConfigureEnum(x => x.UserType);

            builder.Property(x => x.Phone)
                .HasConversion(new PhoneConverter())
                .HasColumnType("nvarchar(20)")
                .IsRequired();

            builder.HasOne(x => x.Credentials)
                .WithOne()
                .HasForeignKey<User>(x => x.CredentialsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
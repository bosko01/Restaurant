using Domain.Models;
using Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.EntityConfiguration
{
    public class UserCredentialsConfiguration : IEntityTypeConfiguration<UserCredentials>
    {
        public void Configure(EntityTypeBuilder<UserCredentials> builder)
        {
            builder.ToTable("UserCredentials");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasConversion(new EmailConverter())
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            builder.Property(x => x.Salt)
                .HasColumnType("nvarchar(255)")
                .IsRequired();
        }
    }
}
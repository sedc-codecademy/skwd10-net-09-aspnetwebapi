using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Models;

namespace Notes.Infrastracture.EntityFramework.EntityTypeConfiguration
{
    public class UserTypeConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Email)
            .HasMaxLength(512)
            .IsRequired();

            builder.Property(p => p.Password)
            .HasMaxLength(1000)
            .IsRequired();
        }
    }
}

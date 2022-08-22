using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SEDC.WebApi.Class06.CodeFirst.Domain.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable(nameof(User))
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(x => x.Phone)
                .HasDefaultValue("000000");
        }
    }
}

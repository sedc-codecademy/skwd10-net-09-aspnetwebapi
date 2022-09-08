using ASPNETAPI_G2_L6.Database.Seeds;
using ASPNETAPI_G2_L6.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPNETAPI_G2_L6.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id).HasName("Id");
            builder.Property(x => x.Username).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Password).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.FirstName).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.LastName).HasColumnType("nvarchar(255)").IsRequired();

            builder.HasData(UsersSeed.USERS);


            builder.HasMany(x => x.Notes)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Models;

namespace Notes.Infrastracture.EntityFramework.EntityTypeConfiguration
{
    public class TagTypeConfigration
        : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x => x.Color).HasMaxLength(50);
            builder.Property(x => x.Value).HasMaxLength(50).IsRequired();

            builder.HasOne(x => x.Note)
                .WithMany(x => x.Tags)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

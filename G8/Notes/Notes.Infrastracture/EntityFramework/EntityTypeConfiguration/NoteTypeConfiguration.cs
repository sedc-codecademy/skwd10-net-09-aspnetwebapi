using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Models;

namespace Notes.Infrastracture.EntityFramework.EntityTypeConfiguration
{
    public class NoteTypeConfiguration :
        IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.Notes)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasQueryFilter(x => !x.IsDeleted); // where isDeleted = 0
        }
    }
}

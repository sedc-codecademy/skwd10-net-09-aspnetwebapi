using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public string Color { get; set; }

        [ForeignKey(nameof(NoteId))]
        public Note Note { get; set; }

        public int NoteId { get; set; }
    }
}

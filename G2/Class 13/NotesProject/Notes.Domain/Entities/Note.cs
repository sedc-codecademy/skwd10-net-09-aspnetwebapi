using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Domain.Entities
{
    public class Note : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int UserId { get; set; }
    }
}

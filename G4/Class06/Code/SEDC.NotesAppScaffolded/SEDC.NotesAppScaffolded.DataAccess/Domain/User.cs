using System;
using System.Collections.Generic;

namespace SEDC.NotesAppScaffolded.DataAccess.Domain
{
    public  class User
    {
        public User()
        {
            Notes = new HashSet<Note>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}

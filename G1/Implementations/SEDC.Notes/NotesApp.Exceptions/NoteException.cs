using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Exceptions
{
    public class NoteException : Exception
    {
        public int? NoteId { get; set; }
        public int? UserId { get; set; }
        public NoteException() : base("There has been an issue with a note!") {}

        public NoteException(int? noteId, int? userId) 
            : base("There has been an issue with a note!")
        {
            NoteId = noteId;
            UserId = userId;
        }

        public NoteException(int? noteId, int? userId, string message) : base(message)
        {
            NoteId = noteId;
            UserId = userId;
        }
    }
}

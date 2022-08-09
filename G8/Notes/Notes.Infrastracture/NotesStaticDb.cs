using Notes.Domain.Models;

namespace Notes.Infrastracture
{
    public static class NotesStaticDb
    {
        public static IList<Note> Notes { get; set; } = new List<Note>();

        public static IList<User> User { get; set; } = new List<User>();
    }
}

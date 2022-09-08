namespace SEDC.NotesApp.Shared.CustomExceptions
{
    public class NoteNotFoundException : Exception
    {
        public NoteNotFoundException(string message) : base(message)
        {

        }
    }
}

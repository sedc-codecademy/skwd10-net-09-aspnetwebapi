

namespace SEDC.NotesApp.Shared.CustomExceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message)
        {

        }
    }
}

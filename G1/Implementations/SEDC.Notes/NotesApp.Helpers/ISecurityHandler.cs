using NotesApp.DataModels;

namespace NotesApp.Helpers
{
    public interface ISecurityHandler
    {
        string GenerateSecurityToken(UserDto user);
    }
}
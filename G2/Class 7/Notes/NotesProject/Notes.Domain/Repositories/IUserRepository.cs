using Notes.Domain.Entities;

namespace Notes.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IReadOnlyList<User>> GetAllUsersAsync();

        Task<User> GetUserAsync(int id);

        Task InsertUserAsync(User newUser);

        Task UpdateUserAsync(User existingUser);

        Task DeleteUserAsync(int id);
    }
}

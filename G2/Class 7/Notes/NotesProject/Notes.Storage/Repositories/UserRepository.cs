using Microsoft.EntityFrameworkCore;
using Notes.Domain.Entities;
using Notes.Domain.Repositories;
using Notes.Storage.Database;

namespace Notes.Storage.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(INotesDbContext dbContext) : base(dbContext)
        {

        }

        public async Task DeleteUserAsync(int id)
        {
            DeleteEntity(id);
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            IQueryable<User> userQuery = GetAll();

            return await userQuery.ToArrayAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await GetById(id).SingleOrDefaultAsync();
        }

        public async Task InsertUserAsync(User newUser)
        {
            InsertEntity(newUser);
        }

        public async Task UpdateUserAsync(User existingUser)
        {
            UpdateEntity(existingUser);
        }
    }
}

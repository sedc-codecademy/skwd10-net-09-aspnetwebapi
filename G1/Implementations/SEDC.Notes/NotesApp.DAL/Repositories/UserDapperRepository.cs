using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using NotesApp.Configurations;
using NotesApp.DataModels;

namespace NotesApp.DAL.Repositories
{
    public class UserDapperRepository
        : IRepository<UserDto>
    {
        private AppSettings settings;

        public UserDapperRepository(IOptions<AppSettings> options)
        {
            settings = options.Value;
        }
        public UserDto? GetById(int id)
        {
            using var connection = new SqlConnection(settings.ConnectionString);
            connection.Open();
            var sql = "Select * From Users u inner join Notes n on u.Id = n.userId where u.Id = @Id";
            return connection.QueryFirstOrDefault<UserDto>(sql, new { Id = id });
        }
        public void Add(UserDto entity)
        {
            using var connection = new SqlConnection(settings.ConnectionString);
            connection.Open();
            var sql = "Insert into Users(UserName, FirstName, LastName, Password) Value(@Username, @FirstName, @LastName, @Password)";
            var rows = connection.Execute(sql, entity);
        }
        public void Delete(UserDto entity)
        {
            using var connection = new SqlConnection(settings.ConnectionString);
            connection.Open();
            var sql = "Delete From Users where Id = @Id";
            var rows = connection.Execute(sql, new { entity.Id});
        }

        public IEnumerable<UserDto> GetAll()
        {
            using var connection = new SqlConnection(settings.ConnectionString);
            connection.Open();
            var sql = "Select * From Users u inner join Notes n on u.Id = n.userId";
            return connection.Query<UserDto>(sql);
        }


        public UserDto GetById(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(UserDto entity)
        {
            using var connection = new SqlConnection(settings.ConnectionString);
            connection.Open();
            var sql = "Update Users set Username = @Username, Password = @Password, Firstname = @FirstName, LastName = @LastName";
            var rows = connection.Query<UserDto>(sql, entity);
        }
    }
}

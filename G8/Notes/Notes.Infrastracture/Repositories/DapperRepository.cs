using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notes.Application.Repositories;
using Notes.Domain.Models;

namespace Notes.Infrastracture.Repositories
{
    public class DapperRepository
        : IRepository<Note>
    {
        private readonly IConfiguration configuration;
        private const string ConnectionString = "DefaultConnectionString";

        public DapperRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Note? GetById(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString(ConnectionString));
            sqlConnection.Open();
            var sql = $@"
    SELECT 
    * 
    FROM 
    [dbo].[Note] n
    INNER JOIN [dbo].[User] u
    On n.UserId = u.Id
    INNER JOIN [dbo].[Tag] t
    ON t.NoteId = n.Id
    where Text Id = @id
    ";
            return sqlConnection.QueryFirstOrDefault<Note>(sql, new
            {
                Id = id
            });
        }
        public Note Create(Note entity)
        {
            using SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString(ConnectionString));
            sqlConnection.Open();
            var sql = "Insert into [dbo].[Note] (Text, Color, UserId) Values(@Text, @Color, @UserId); SELECT SCOPE_IDENTITY();";
            var id = sqlConnection.ExecuteScalar<int>(sql, new
            {
                entity.Text,
                entity.Color,
                UserId = entity.User.Id
            });
            entity.Id = id;
            return entity;
        }

        public Note Delete(Note entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Note> GetAll()
        {
            throw new NotImplementedException();
        }



        public Note Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}

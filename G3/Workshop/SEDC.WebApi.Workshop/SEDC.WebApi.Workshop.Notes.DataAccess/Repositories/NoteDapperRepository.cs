using Dapper;
using Microsoft.Extensions.Options;
using SEDC.WebApi.Workshop.Notes.Common.Models;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using System.Data.SqlClient;

namespace SEDC.WebApi.Workshop.Notes.DataAccess.Repositories
{
    public class NoteDapperRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteDapperRepository(IOptions<AppSettings> options)
        {
            _connectionString = options.Value.ConnectionString;
        }

        public int Delete(Note entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Note> FilterBy(Func<Note, bool> filter)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            var sqlQuery = "select * from dbo.Note";
            
            conn.Open();
            return conn.Query<Note>(sqlQuery).Where(filter);
        }

        public IEnumerable<Note> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"select * from dbo.Note";
                conn.Open();
                var notes = conn.Query<Note>(sqlQuery);
                return notes;
            }
        }

        public Note GetById(int id)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"select * from dbo.Note n
                                where Id = @id";
                conn.Open();
                return conn
                    .Query<Note>(sqlQuery, new { id })
                    .FirstOrDefault();
            }
        }

        public int Insert(Note entity)
        {
            throw new NotImplementedException();
        }

        public int Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}

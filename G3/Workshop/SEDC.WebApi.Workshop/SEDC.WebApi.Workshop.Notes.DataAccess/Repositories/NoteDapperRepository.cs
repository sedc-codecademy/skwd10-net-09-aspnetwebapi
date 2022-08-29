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
            using SqlConnection conn = new SqlConnection(_connectionString);
            var query = @"delete from dbo.Note
                                where Id = @id";

            conn.Open();
            conn.Query(query, new { id = entity.Id });
            return entity.Id;
        }

        public IEnumerable<Note> FilterBy(Func<Note, bool> filter)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            var sqlQuery = "select * from dbo.Note";
            
            conn.Open();

            //var query = @"select * from dbo.Note
            //            where Id in @ids";
            //var ids = new int[] { 1, 2, 3, 4 };

            //var notes = conn.Query<Note>(query, 
            //    new { ids = ids.ToArray() });

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
            using SqlConnection conn = new SqlConnection(_connectionString);
            var sqlQuery = @"insert into dbo.Note([Text], [Color], [Tag], [UserId])
                    values (@text, @color, @tag, @userId)";
            
            conn.Open();
            conn.Query(sqlQuery,
                new
                {
                    text = entity.Text,
                    color = entity.Color,
                    tag = entity.Tag,
                    userId = entity.UserId
                });
            return entity.Id;
        }

        public int Update(Note entity)
        {
            using SqlConnection conn = new SqlConnection (_connectionString);
            var sqlQuery = @"UPDATE dbo.Note
                    SET Text = @noteText, 
                        Color = @noteColor, 
                        Tag = @noteTag, 
                        UserId = @noteUserId
                    WHERE Id = @id";
            conn.Open();

            conn.Query(sqlQuery, new
            {
                id = entity.Id,
                text = entity.Text,
                color = entity.Color,
                tag = entity.Tag,
                userId = entity.UserId
            });
            return entity.Id;
        }
    }
}

using Dapper;
using SEDC.WebApi.Class08.Adonet.Models;
using System.Data.SqlClient;

namespace SEDC.WebApi.Class08.Adonet.Services
{
    public class NoteDapperService
    {
        private readonly string
            _connectionString =
            @"Server=(LocalDb)\MSSQLLocalDB;Database=NoteDb;Trusted_Connection=True";

        public IEnumerable<Note> GetAll()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery = "select * from dbo.Note";

                var notes = connection.Query<Note>(sqlQuery);
                return notes;
            }
        }

        public Note GetByUserIdAndNoteId(int userId, int noteId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sqlQuery = @"select * from dbo.Note 
                                where Id = @noteId and UserId = @userId";

                var note = connection
                    .Query<Note>(
                        sqlQuery,
                        new
                        {
                            userId,
                            noteId
                        }).FirstOrDefault();

                return note;
            }
        }

        public void Add(string text, string color, int tag, int userId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                //var sqlQuery = @"
                //    INSERT INTO [dbo].[Note]([Text], [Color], [Tag], [UserId]) 
                //    VALUES (@text, @color, @tag, @userId)";

                //connection.Query(sqlQuery,
                //    new
                //    {
                //        text,
                //        color,
                //        tag,
                //        userId
                //    });


                var sqlQuery = @"
                    INSERT INTO [dbo].[Note]([Text], [Color], [Tag], [UserId]) 
                    VALUES (@description, @boja, @tagType, @user)";

                connection.Query(sqlQuery,
                    new
                    {
                        description = text,
                        boja = color,
                        tagType = tag,
                        user = userId
                    });
            }
        }
    }
}

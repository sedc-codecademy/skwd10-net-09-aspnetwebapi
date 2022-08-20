using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notes.Application.Repositories;
using Notes.Domain.Models;

namespace Notes.Infrastracture.Repositories
{
    // Connection 
    // Open Connection
    // create command
    // add parametars
    // exec reader or no query
    public class NoteAdoRepository
        : IRepository<Note>
    {
        private readonly IConfiguration configuration;
        private const string ConnectionString = "DefaultConnectionString";
        public NoteAdoRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // '
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
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader? reader = command.ExecuteReader();
            Note note = null;
            while (reader.Read())
            {
                if(note == null)
                {
                    note = new Note
                    {
                        Id = (int)reader[nameof(Note.Id)],
                        Color = (string)reader[nameof(Note.Color)],
                        Text = (string)reader[nameof(Note.Text)],
                    };
                }
                else
                {
                    id = (int)reader[nameof(Note.Id)];
                    if(id != note.Id)
                    {
                        // exception
                    }
                }

                if (note.User == null)
                {
                    note.User = new User
                    {
                        Email = (string)reader[nameof(User.Email)]
                    };
                }
                var tag = new Tag
                {
                    Id = (int)reader[nameof(Tag.Id)],
                };
                note.Tags.Add(tag);
            }
            sqlConnection.Close();
            return note;
        }

        public Note Create(Note entity)
        {
            using SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString(ConnectionString));
            sqlConnection.Open();
            var sql = @"
INSERT INTO [dbo].[Note] (Text, Color,UserId) VALUES(@text, @color, @userId);
SELECT SCOPE_IDENTITY();";
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@color", entity.Color);
            command.Parameters.AddWithValue("@userId", entity.User.Id);
            var id = (int)command.ExecuteScalar();
  
            sqlConnection.Close();
            entity.Id = id;
            return entity;
        }   
        public Note Delete(Note entity)
        {
            using SqlConnection sqlConnection = new SqlConnection(configuration.GetConnectionString(ConnectionString));
            sqlConnection.Open();
            var sql = $@"DELETE FROM [dbo].[Note] where Id = @id";
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", entity.Id);
            command.ExecuteNonQuery();

            sqlConnection.Close();
            return entity;
        }

        public IQueryable<Note> GetAll()
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
    order by n.text
    ";
            SqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = sql;
            SqlDataReader? reader = command.ExecuteReader();
            var result = new List<Note>();
            Note note = null;
            while (reader.Read())
            {
                if (note == null)
                {
                    note = new Note
                    {
                        Id = (int)reader[nameof(Note.Id)],
                        Color = (string)reader[nameof(Note.Color)],
                        Text = (string)reader[nameof(Note.Text)],
                    };
                }
                else
                {
                    
                    int id = (int)reader[nameof(Note.Id)];
                    if (id != note.Id)
                    {
                        if(!result.Any(x => x.Id == note.Id))
                        {
                            result.Add(note);
                        }

                        if(result.Any(x => x.Id == id))
                        {
                            note = result.First(x => x.Id == id);
                        }
                        else
                        {
                            note = new Note() { };
                        }
                    }
                }

                if (note.User == null)
                {
                    note.User = new User
                    {
                        Email = (string)reader[nameof(User.Email)]
                    };
                }
                var tag = new Tag
                {
                    Id = (int)reader[nameof(Tag.Id)],
                };
                note.Tags.Add(tag);
            }
            sqlConnection.Close();
            return result.AsQueryable();
        }



        public Note Update(Note entity)
        {
            throw new NotImplementedException();
        }
    }
}

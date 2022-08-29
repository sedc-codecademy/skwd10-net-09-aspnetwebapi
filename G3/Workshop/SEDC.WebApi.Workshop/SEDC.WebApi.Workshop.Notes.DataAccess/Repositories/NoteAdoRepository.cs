using Microsoft.Extensions.Options;
using SEDC.WebApi.Workshop.Notes.Common.Models;
using SEDC.WebApi.Workshop.Notes.DataModels.Models;
using System.Data.SqlClient;

namespace SEDC.WebApi.Workshop.Notes.DataAccess.Repositories
{
    public class NoteAdoRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteAdoRepository(IOptions<AppSettings> settings)
        {
            _connectionString = settings.Value.ConnectionString;
        }

        public int Delete(Note entity)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"delete from dbo.Note
                                where Id = @id";

                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.Parameters.AddWithValue("@id", entity.Id);

                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<Note> FilterBy(Func<Note, bool> filter)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from dbo.Note";

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sqlQuery;
                    cmd.Connection = connection;

                    var reader = cmd.ExecuteReader();

                    List<Note> notes = new List<Note>();
                    while (reader.Read())
                    {
                        var note = new Note
                        {
                            Id = (int)reader["Id"],
                            Text = reader["Text"].ToString(),
                            Color = reader["Color"].ToString(),
                            Tag = (int)reader["Tag"],
                            UserId = (int)reader["UserId"]
                        };
                        notes.Add(note);
                    }

                    return notes.Where(filter);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Note> GetAll()
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "select * from dbo.Note";

                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sqlQuery;
                    cmd.Connection = connection;

                    var reader = cmd.ExecuteReader();

                    List<Note> notes = new List<Note>();
                    while(reader.Read())
                    {
                        var note = new Note
                        {
                            Id = (int)reader["Id"],
                            Text = reader["Text"].ToString(),
                            Color = reader["Color"].ToString(),
                            Tag = (int)reader["Tag"],
                            UserId = (int)reader["UserId"]
                        };
                        notes.Add(note);
                    }

                    return notes;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Note GetById(int id)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"select * from dbo.Note n
                                   where n.Id = @id";

                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sqlQuery;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@id", id);

                    var reader = cmd.ExecuteReader();
                    var notes = new List<Note>();
                    while (reader.Read())
                    {
                        var note = new Note
                        {
                            Id = (int)reader["Id"],
                            Text = reader["Text"].ToString(),
                            Color = reader["Color"].ToString(),
                            Tag = (int)reader["Tag"],
                            UserId = (int)reader["UserId"]
                        };
                        notes.Add(note);
                    }

                    return notes.FirstOrDefault();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public int Insert(Note entity)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"insert into dbo.Note([Text], [Color], [Tag], [UserId])
                    values (@text, @color, @tag, @userId)";

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sqlQuery;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@text", entity.Text);
                    cmd.Parameters.AddWithValue("@color", entity.Color);
                    cmd.Parameters.AddWithValue("@tag", entity.Tag);
                    cmd.Parameters.AddWithValue("@userId", entity.UserId);

                    var rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }

            }
        }

        public int Update(Note entity)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = @"UPDATE dbo.Note
                    SET Text = @noteText, 
                        Color = @noteColor, 
                        Tag = @noteTag, 
                        UserId =@noteUserId
                    WHERE Id = @id";

                try
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(sqlQuery, connection);
                    cmd.Parameters.AddWithValue("@id", entity.Id);
                    cmd.Parameters.AddWithValue("@noteText", entity.Text);
                    cmd.Parameters.AddWithValue("@noteColor", entity.Color);
                    cmd.Parameters.AddWithValue("@noteTag", entity.Tag);
                    cmd.Parameters.AddWithValue("@noteUserId", entity.UserId);

                    var rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}

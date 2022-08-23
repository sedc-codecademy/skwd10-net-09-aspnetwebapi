using ASPNETAPI_G2_L6.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNETAPI_G2_L6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {

        private readonly string _dbConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NotesDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAllNotesAsync()
        {
            using SqlConnection sqlConnection = new SqlConnection(_dbConnectionString);
            await sqlConnection.OpenAsync();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = "select * from Notes";

            using SqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync();

            List<Note> notes = new List<Note>();
            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string text = dataReader.GetString(1);
                string color = dataReader.GetString(2);
                int tag = dataReader.GetInt32(3);
                int userId = dataReader.GetInt32(4);
                
                notes.Add(new Note
                {
                    Id = id,
                    Text = text,
                    Color = color,
                    Tag = tag,
                    UserId = userId
                });
            }

            await sqlConnection.CloseAsync();
            return Ok(notes);
        }

        [Route("text")]
        [HttpGet]
        public async Task<IActionResult> GetNoteByTextAsync([FromQuery] string noteText)
        {
            using SqlConnection sqlConnection = new SqlConnection(_dbConnectionString);
            await sqlConnection.OpenAsync();

            using SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection; // '%' OR TRUE '%' FOR SQL INJECTION
            sqlCommand.CommandText = "SELECT * FROM Notes WHERE Text = @noteText";
            sqlCommand.Parameters.AddWithValue("@noteText", noteText);

            using SqlDataReader dataReader = await sqlCommand.ExecuteReaderAsync();

            List<Note> notes = new List<Note>();
            while (dataReader.Read())
            {
                int id = dataReader.GetInt32(0);
                string text = dataReader.GetString(1);
                string color = dataReader.GetString(2);
                int tag = dataReader.GetInt32(3);
                int userId = dataReader.GetInt32(4);

                notes.Add(new Note
                {
                    Id = id,
                    Text = text,
                    Color = color,
                    Tag = tag,
                    UserId = userId
                });
            }

            await sqlConnection.CloseAsync();
            return Ok(notes);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> CreateNoteAsync([FromBody] CreateNoteDto note)
        {

            using SqlConnection sqlConnection = new SqlConnection(_dbConnectionString);
            await sqlConnection.OpenAsync();

            try
            {
                using SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = $"INSERT INTO Notes(Text, Color, Tag, UserId) VALUES ('{note.Text}', '{note.Color}', {note.Tag}, {note.UserId});";

                await sqlCommand.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await sqlConnection.CloseAsync();
            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetNoteById([FromRoute]int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(_dbConnectionString);
            await sqlConnection.OpenAsync();

            var result = await sqlConnection.QueryAsync<Note>("select * from Notes where Id = @id", new { id });

            return Ok(result);
        }

        [Route("createWithDapper")]
        [HttpPost]
        public async Task<IActionResult> CreateNoteWithDapperAsync([FromBody] CreateNoteDto note)
        {

            using SqlConnection sqlConnection = new SqlConnection(_dbConnectionString);
            await sqlConnection.OpenAsync();

            try
            {
                var result = sqlConnection.ExecuteAsync($"INSERT INTO Notes(Text, Color, Tag, UserId) VALUES('{note.Text}', '{note.Color}', {note.Tag}, {note.UserId})");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            await sqlConnection.CloseAsync();
            return Ok();
        }
    }
}

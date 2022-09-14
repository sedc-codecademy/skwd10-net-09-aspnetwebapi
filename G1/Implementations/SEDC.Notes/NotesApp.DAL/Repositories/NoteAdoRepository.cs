using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using NotesApp.Configurations;
using NotesApp.DataModels;
using NotesApp.InerfaceModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.DAL.Repositories
{
    public class NoteAdoRepository
        : IRepository<NoteDto>
    {
        private AppSettings settings;

        public NoteAdoRepository(IOptions<AppSettings> options)
        {
            settings = options.Value;
        }
        // getByText
        // 
        public NoteDto GetById(int id, int userId)
        {
            using SqlConnection sqlConnection = new SqlConnection(settings.ConnectionString);
            sqlConnection.Open();

            SqlCommand? command = sqlConnection.CreateCommand();
            var sql = @"
            Select 
                 n.Id as NoteId,
                 n.Color,
                 n.Tag,
                 u.Id as UserId,
                 u.UserName,
                 u.FirstName,
                 u.LastName
            from 
                Notes n
            inner join
                Users u
            on u.Id = n.userId
            where 
                u.Id = @id and userId = @userId";
            command.CommandText = sql;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@userId", userId);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            var user = new UserDto()
            {
                Id = int.Parse(reader[nameof(UserDto.Id)]!.ToString()!),
                FirstName = reader[nameof(UserDto.FirstName)].ToString()!,
                LastName = reader[nameof(UserDto.LastName)].ToString()!,
                Username = reader[nameof(UserDto.Username)].ToString()!,
            };
            var note = new NoteDto
            {
                Id = int.Parse(reader[nameof(NoteDto.Id)].ToString()!)!,
                Color = reader[nameof(NoteDto.Color)].ToString()!,
                Tag = int.Parse(reader[nameof(NoteDto.Color)].ToString()!),
                User = user
            };
            return note;
        }

        public void Add(NoteDto entity)
        {
            using SqlConnection sqlConnection = new SqlConnection(settings.ConnectionString);
            sqlConnection.Open();

            var command = sqlConnection.CreateCommand();
            var sql = "INSERT INTO NOTES(Text, Color, Tag) VALUES(@Text, @Color, @Tag)";
            command.CommandText = sql;
            command.Parameters.AddWithValue("@Text", entity.Text);
            command.Parameters.AddWithValue("@Color", entity.Color);
            command.Parameters.AddWithValue("@Tag", entity.Tag);

            var rows = command.ExecuteNonQuery();
        }

        public void Delete(NoteDto entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public NoteDto GetById(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(NoteDto entity)
        {
            throw new NotImplementedException();
        }
    }
}

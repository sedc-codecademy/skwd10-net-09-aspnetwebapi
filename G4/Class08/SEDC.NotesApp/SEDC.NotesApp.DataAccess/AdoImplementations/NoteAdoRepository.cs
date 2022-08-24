﻿using Microsoft.Data.SqlClient;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;

namespace SEDC.NotesApp.DataAccess.AdoImplementations
{
    public class NoteAdoRepository : IRepository<Note>
    {
        private string _connectionString;

        public NoteAdoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Note entity)
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            //4. connect the command 
            command.Connection = sqlConnection;

            command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
                                  "VALUES(@text, @priority, @tag, @userId)"; //value from outside (from the client), that is why we use parameters

            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);

            //BAD EXAMPLE - POTENTIAL SQL INJECTION ATTACK
            //command.CommandText = "INSERT INTO dbo.Notes(Text, Priority, Tag, UserId)" +
            //                     $"VALUES({entity.Text}, @priority, @tag, @userId)";

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public void Delete(Note entity)
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            //4. connect the command 
            command.Connection = sqlConnection;

            command.CommandText = "DELETE FROM dbo.Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public List<Note> GetAll()
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            //4. connect the command 
            command.Connection = sqlConnection;
            //5. write the command
            command.CommandText = "SELECT * FROM dbo.Notes";

            List<Note> notesDb = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (Priority)sqlDataReader["Priority"],
                    Tag = (Tag)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                    //User = new User
                    //{
                    //    FirstName = (string)sqlDataReader["FirstName"]
                    //}
                });
            }

            //6.close the connection!!!
            sqlConnection.Close();

            //using(SqlConnection connection = new SqlConnection(_connectionString))
            //{
            //    connection.Open();
            //    //3. Create sql command
            //    SqlCommand command = new SqlCommand();
            //    //4. connect the command 
            //    command.Connection = sqlConnection;
            //    //5. write the command
            //    command.CommandText = "SELECT * FROM dbo.Notes";

            //    List<Note> notesDb = new List<Note>();

            //    SqlDataReader sqlDataReader = command.ExecuteReader();
            //    while (sqlDataReader.Read())
            //    {
            //        notesDb.Add(new Note()
            //        {
            //            Id = (int)sqlDataReader["Id"],
            //            Text = (string)sqlDataReader["Text"],
            //            Priority = (Priority)sqlDataReader["Priority"],
            //            Tag = (Tag)sqlDataReader["Tag"],
            //            UserId = (int)sqlDataReader["UserId"]
            //            //User = new User
            //            //{
            //            //    FirstName = (string)sqlDataReader["FirstName"]
            //            //}
            //        });
            //    }
            //}

            return notesDb;
        }

        public Note GetById(int id)
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;
            //command.CommandText = $"SELECT * FROM dbo.Notes WHERE Id = {id}";
            command.CommandText = "SELECT * FROM dbo.Notes WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);

            List<Note> notesDb = new List<Note>();

            SqlDataReader sqlDataReader = command.ExecuteReader();
            while (sqlDataReader.Read())
            {
                notesDb.Add(new Note()
                {
                    Id = (int)sqlDataReader["Id"],
                    Text = (string)sqlDataReader["Text"],
                    Priority = (Priority)sqlDataReader["Priority"],
                    Tag = (Tag)sqlDataReader["Tag"],
                    UserId = (int)sqlDataReader["UserId"]
                    //User = new User
                    //{
                    //    FirstName = (string)sqlDataReader["FirstName"]
                    //}
                });
            }

            //6.close the connection!!!
            sqlConnection.Close();

            return notesDb.FirstOrDefault();
        }

        public void Update(Note entity)
        {
            //1. Create new connection to the SQL db
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            //2. Open the connection
            sqlConnection.Open();
            //3. Create sql command
            SqlCommand command = new SqlCommand();
            command.Connection = sqlConnection;

            command.CommandText = "UPDATE dbo.Notes SET Text = @text, Tag = @tag, Priority = @priority, UserId = @userId" +
                " WHERE Id = @id";

            command.Parameters.AddWithValue("@text", entity.Text);
            command.Parameters.AddWithValue("@priority", entity.Priority);
            command.Parameters.AddWithValue("@tag", entity.Tag);
            command.Parameters.AddWithValue("@userId", entity.UserId);
            command.Parameters.AddWithValue("@id", entity.Id);

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}

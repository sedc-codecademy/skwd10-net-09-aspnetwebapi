using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Domain.Models
{
    // Id
    // [Entity]Id => UserId
    public class User
        : IEntity
    {
        public User()
        {

        }
        public User(string username, string password, string firstName, string lastName, string email)
        {
            Username = username;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        
        public string? ForgotPasswordCode { get; private set; }

        public DateTime? ForgotPasswordCodeCreated { get; private set; }

        public string Email { get; set; } = string.Empty;

        public ICollection<Note> Notes { get; set; } = new List<Note>();

        public void SetForgotPasswordCode(string code)
        {
            ForgotPasswordCode = code;
            ForgotPasswordCodeCreated = DateTime.Now;
        }

        public void ClearForgotPasswordCode()
        {
            ForgotPasswordCode = null;
            ForgotPasswordCodeCreated = null;
        }
        public void Test(params int[] numbers)
        {

        }
    }
}

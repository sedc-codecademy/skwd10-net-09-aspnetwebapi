﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Models
{
    public class User
    {
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
    }
}

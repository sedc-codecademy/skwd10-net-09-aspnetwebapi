﻿namespace Notes.Contracts.DTOs.UserDtos
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Role { get; set; }
    }
}

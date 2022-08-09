﻿namespace ASPNETAPI_G2_L3.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();
    }
}

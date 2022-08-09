using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Domain.Models
{
    public class User
    {
        //User:
        //    Id
        //    Username
        //    Password
        //    FirstName
        //    LastName
        //    Notes

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IList<Note> Notes { get; set; }
    }
}

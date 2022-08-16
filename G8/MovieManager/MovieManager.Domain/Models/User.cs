using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Domain.Models
{
    public class User
    {
        public User(string username, string password, string firstname, string lastname)
        {
            Username = username;
            Password = password;
            FirstName = firstname;
            LastName = lastname;
            Movies = new List<Movie>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}"; 
        public IList<Movie> Movies { get; set; }
        
    }
}

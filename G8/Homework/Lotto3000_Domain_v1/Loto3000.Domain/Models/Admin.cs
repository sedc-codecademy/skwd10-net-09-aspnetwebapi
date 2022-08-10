using Loto3000.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Domain.Models
{
    public class Admin
    {
        public Admin() { }
        public Admin(int id, string name, string pw)
        {
            Id = id;
            Name = name;
            Username = $"Admin{name}";
            Password = pw;
            AuthorizationCode = pw;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
        public string AuthorizationCode { get; set; }
    }
}

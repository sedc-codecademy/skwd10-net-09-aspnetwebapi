using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Exceptions
{
    public class UserNotAllowedException : Exception
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public UserNotAllowedException() : base("There has been an issue with a user!") {}

        public UserNotAllowedException(int? userId, string name) 
            : base("There has been an issue with a user!")
        {
            UserId = userId;
            Name = name;    
        }

        public UserNotAllowedException(int? userId, string name, string message) 
            : base(message)
        {
            UserId = userId;
            Name = name;
        }
    }
}

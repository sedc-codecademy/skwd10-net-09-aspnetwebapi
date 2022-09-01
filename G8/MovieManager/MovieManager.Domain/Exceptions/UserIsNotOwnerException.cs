using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Domain.Exceptions
{
    public class UserIsNotOwnerException : Exception
    {
        public int UserId { get; set; }

        public UserIsNotOwnerException()
        {
        }

        public UserIsNotOwnerException(int userId)
        {
            UserId = userId;
        }

        public UserIsNotOwnerException(string? message, int userId) : base(message)
        {
            UserId = userId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Services
{
    public interface IEmailSender
    {
        public string SendMail(string email, string body, string subject);
    }
}

using Notes.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Infrastracture.EmailSender
{
    public class EmailSender
        : IEmailSender
    {
        public string SendMail(string email, string body, string subject)
        {
            return string.Empty;
        }
    }
}

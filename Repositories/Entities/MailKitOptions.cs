using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Entities
{
    public class MailKitOptions
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}

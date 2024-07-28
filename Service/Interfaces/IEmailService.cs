using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmailService
    {
        Task SendMagicLink(string email, string token,int id);
        Task SendMailingList(List<string> recipients, string subject, string body);
        Task SendGeneral(string toEmail, string subject, string body);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IMailKitProvider
    {
        //SmtpClient GetSmtpClient();
        MailKit.Net.Smtp.SmtpClient GetSmtpClient();
    }
}

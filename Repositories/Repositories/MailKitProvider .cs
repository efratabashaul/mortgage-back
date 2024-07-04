using Microsoft.Extensions.Options;
using Repositories.Entities;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;



namespace Repositories.Repositories
{
    public class MailKitProvider : IMailKitProvider
    {

        private readonly MailKitOptions _options;

        public MailKitProvider(IOptions<MailKitOptions> options)
        {
            _options = options.Value;
        }
        public MailKit.Net.Smtp.SmtpClient GetSmtpClient()
        {
            var smtpClient = new MailKit.Net.Smtp.SmtpClient(); // Fully qualified name
            smtpClient.Connect(_options.SmtpServer, _options.SmtpPort, SecureSocketOptions.StartTls);
            smtpClient.Authenticate(_options.Username, _options.Password);
            return smtpClient;
        }
    }
}

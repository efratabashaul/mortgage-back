using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Service.Interfaces;
using Microsoft.Extensions.Options; 
using MimeKit;
using Repositories.Entities;
using Repositories.Interface;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMailKitProvider _mailKitProvider;
        private readonly MailKitOptions _options;

        public EmailService(IMailKitProvider mailKitProvider, IOptions<MailKitOptions> options)
        {
            _mailKitProvider = mailKitProvider;
            _options = options.Value;
        }

        public async Task SendMagicLink(string toEmail, string token,int id)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Y.B Mortgages", _options.From));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Your Magic Login Link";

            // message.Body = new TextPart("html")
            // {
            //     //Text = $@"
            //     //<html>
            //     //<body>
            //     //    <p>Hello,</p>
            //     //    <p>Click <a href='https://yourapp.com/magic-link?token={token}'>here</a> to log in.</p>
            //     //</body>
            //     //</html>"
            //
            //
            // };


            message.Body = new TextPart("html")
            {
                Text = $@"
        <html>
        <body style='margin: 0; padding: 0;'>
            <table width='100%' cellspacing='0' cellpadding='0'>
                <tr>
                    <td align='center' style='padding: 10px;'>
                        <table width='600' cellspacing='0' cellpadding='0' style='border: 1px solid #cccccc;'>
                            <tr>
                                <td align='center' style='padding: 40px 0 30px 0; background-color: #f2f2f2;'>
                                    <h1 style='margin: 0; font-size: 48px; font-family: Arial, sans-serif;'>
                                        <span style='color: rgb(183, 182, 182);'>Y</span>.<span style='color: rgba(255, 68, 0, 0.749);'>B</span>
                                    </h1>
                                </td>
                            </tr>
                            <tr>
                                <td align='center' style='padding: 30px; background-color: #ffffff;'>
                                    <h2 style='color: rgba(255, 115, 0, 0.955); font-size: 32px; font-family: Arial, sans-serif;'>
                                        Welcome!
                                    </h2>
                                    <p style='font-family: Arial, sans-serif; font-size: 16px;'>
                                        Click <a href='http://localhost:4200/magic-link?token={token}&id={id}' style='color: #ff7300;'>here</a> to log in.
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td align='center' style='padding: 20px; background-color: #f2f2f2;'>
                                    <p style='margin: 0; font-family: Arial, sans-serif; font-size: 12px; color: #666666;'>
                                        © Your App. All rights reserved.
                                    </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </body>
        </html>"
            };
            //'https://yourapp.com/magic-link?token={token}'

            using (var client = _mailKitProvider.GetSmtpClient())
            {
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}

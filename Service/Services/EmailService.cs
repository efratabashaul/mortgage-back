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
using MailKit.Net.Smtp;

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
            /* Click <a href='http://localhost:4200/magic-link?token={token}&id={id}' style='color: #ff7300;'>here</a> to log in.*/

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
                                        Click <a href='http://localhost:4200/leadLogin' style='color: #ff7300;'>here</a> to log in.

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
        public async Task SendMailingList(List<string>recipients, string subject, string body)
        {
            if (subject == "")
                subject = " ";
            if(body =="")
                body= " ";
            int batchSize = 100; // size of the group according to the limitation of the server
            List<List<string>> batches = CreateBatches(recipients, batchSize);

            try
            {
                foreach (var batch in batches)
                {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Y.B Mortgages", _options.From));
                    message.Subject = subject;

                    var bodyBuilder = new BodyBuilder { TextBody = body };
                    message.Body = bodyBuilder.ToMessageBody();

                    foreach (var recipient in batch)
                    {
                        message.Bcc.Add(new MailboxAddress("", recipient));
                    }

                    using (var client = _mailKitProvider.GetSmtpClient())
                    {
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }
                }

                Console.WriteLine("Emails sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }

        public async Task SendGeneral(string toEmail, string subject, string body)
        {
            try
            {
                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Y.B Mortgages", _options.From));
                    message.Subject = subject;
                //var bodyBuilder = new BodyBuilder { TextBody = body };
                //message.Body = bodyBuilder.ToMessageBody();
                message.Body = new TextPart("html")
                {
                    Text = body
                };
                message.To.Add(new MailboxAddress("", toEmail));

                    using (var client = _mailKitProvider.GetSmtpClient())
                    {
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }
                Console.WriteLine("Emails sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email. Error: " + ex.Message);
            }
        }

        static List<List<string>> CreateBatches(List<string> source, int batchSize)
        {
            var batches = new List<List<string>>();
            for (int i = 0; i < source.Count; i += batchSize)
            {
                batches.Add(source.GetRange(i, Math.Min(batchSize, source.Count - i)));
            }
            return batches;
        }
    }
    }

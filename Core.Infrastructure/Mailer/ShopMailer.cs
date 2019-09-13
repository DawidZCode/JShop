using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infrastructure.Mailer
{
    public class ShopMailer : IShopMailer
    {
        IConfiguration _configuration;

        public ShopMailer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async System.Threading.Tasks.Task<bool> SendMailAsync(string mailToName, string mailTo, string title, string desctiption)
        {
            var msg = PrepateBaseMailMessage(mailToName, mailTo, title, desctiption);

            var client = new SendGridClient(_configuration.GetSection("Mailer").GetSection("SendgridAPIKEY").Value);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;
            else
                return false;
        }



        public async System.Threading.Tasks.Task<bool> SendMailWithAttachment(string mailToName, string mailTo, string title, string desctiption,string fileName,  string attachmentBase64)
        {
            var msg = PrepateBaseMailMessage(mailToName, mailTo, title, desctiption);
            msg.AddAttachment(fileName, attachmentBase64);

            var client = new SendGridClient(_configuration.GetSection("Mailer").GetSection("SendgridAPIKEY").Value);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                return true;
            else
                return false;
        }

        private SendGridMessage PrepateBaseMailMessage(string mailToName, string mailTo, string title, string desctiption)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(_configuration.GetSection("Mailer").GetSection("SendgridMailFrom").Value, _configuration.GetSection("Mailer").GetSection("SendgridMailFromName").Value));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress(mailTo, mailToName)
            };
            msg.AddTos(recipients);

            msg.SetSubject(title);

            msg.AddContent(desctiption, desctiption);
            return msg;
        }
    }
}

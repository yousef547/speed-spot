using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace portal.speedspot.WebAPI.Helpers
{
    public class EmailSender
    {
        private IConfiguration Configuration { get; set; }
        public EmailSender(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Send(string subject, string body, string mailToEmail, string mailToName, bool isAllBcc = false)
        {
            var configSection = Configuration.GetSection("MailSettings");
            SmtpClient client = new SmtpClient(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")))
            {
                EnableSsl = bool.Parse(configSection.GetValue<string>("EnableSsl")),
                Credentials = new NetworkCredential(configSection.GetValue<string>("SenderUsername"), configSection.GetValue<string>("SenderPassword"))
            };

            MailMessage message = new MailMessage();
            message.From = new MailAddress(configSection.GetValue<string>("EmailFrom"), configSection.GetValue<string>("EmailFromName"));

            message.Body = body;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;

            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;

            MailAddress emailTo = new MailAddress(mailToEmail, mailToName);
            if (isAllBcc)
                message.Bcc.Add(emailTo);
            else
                message.To.Add(emailTo);

            message.Bcc.Add(configSection.GetValue<string>("BccEmails"));

            message.ReplyToList.Add(new MailAddress(configSection.GetValue<string>("SystemReplyTo"), configSection.GetValue<string>("SystemReplyToName")));

            client.Send(message);
        }

        public void Send(string subject, string body, Dictionary<string, string> mailToEmails, bool isAllBcc = false)
        {
            var configSection = Configuration.GetSection("MailSettings");
            SmtpClient client = new SmtpClient(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")))
            {
                EnableSsl = bool.Parse(configSection.GetValue<string>("EnableSsl")),
                Credentials = new NetworkCredential(configSection.GetValue<string>("SenderUsername"), configSection.GetValue<string>("SenderPassword"))
            };

            MailMessage message = new MailMessage();
            message.From = new MailAddress(configSection.GetValue<string>("EmailFrom"), configSection.GetValue<string>("EmailFromName"));

            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;

            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;

            foreach (var email in mailToEmails)
            {
                MailAddress emailTo = new MailAddress(email.Key, email.Value);
                if (isAllBcc)
                    message.Bcc.Add(emailTo);
                else
                    message.To.Add(emailTo);
            }

            message.Bcc.Add(configSection.GetValue<string>("BccEmails"));

            message.ReplyToList.Add(new MailAddress(configSection.GetValue<string>("SystemReplyTo"), configSection.GetValue<string>("SystemReplyToName")));

            client.Send(message);
        }
    }
}

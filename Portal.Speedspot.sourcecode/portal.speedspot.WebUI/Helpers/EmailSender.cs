using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Logging;
using MailKit.Security;

namespace portal.speedspot.WebUI.Helpers
{
    public class EmailSender
    {
        private readonly ILogger _logger;
        private IConfiguration Configuration { get; set; }
        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }
        //public void Send(string subject, string body, string mailToEmail, string mailToName, bool isAllBcc = false)
        //{
        //    var configSection = Configuration.GetSection("MailSettings");
        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")))
        //    {
        //        EnableSsl = bool.Parse(configSection.GetValue<string>("EnableSsl")),
        //        Credentials = new NetworkCredential(configSection.GetValue<string>("SenderUsername"), configSection.GetValue<string>("SenderPassword"))
        //    };

        //    MailMessage message = new MailMessage();
        //    message.From = new MailAddress(configSection.GetValue<string>("EmailFrom"), configSection.GetValue<string>("EmailFromName"));

        //    message.Body = body;
        //    message.IsBodyHtml = true;
        //    message.BodyEncoding = Encoding.UTF8;

        //    message.Subject = subject;
        //    message.SubjectEncoding = Encoding.UTF8;

        //    MailAddress emailTo = new MailAddress(mailToEmail, mailToName);
        //    if (isAllBcc)
        //        message.Bcc.Add(emailTo);
        //    else
        //        message.To.Add(emailTo);

        //    message.Bcc.Add(configSection.GetValue<string>("BccEmails"));

        //    message.ReplyToList.Add(new MailAddress(configSection.GetValue<string>("SystemReplyTo"), configSection.GetValue<string>("SystemReplyToName")));

        //    client.Send(message);
        //}

        //public void Send(string subject, string body, Dictionary<string, string> mailToEmails, bool isAllBcc = false)
        //{
        //    var configSection = Configuration.GetSection("MailSettings");
        //    System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")))
        //    {
        //        EnableSsl = bool.Parse(configSection.GetValue<string>("EnableSsl")),
        //        Credentials = new NetworkCredential(configSection.GetValue<string>("SenderUsername"), configSection.GetValue<string>("SenderPassword"))
        //    };

        //    MailMessage message = new MailMessage();
        //    message.From = new MailAddress(configSection.GetValue<string>("EmailFrom"), configSection.GetValue<string>("EmailFromName"));

        //    message.Body = body;
        //    message.BodyEncoding = Encoding.UTF8;

        //    message.Subject = subject;
        //    message.SubjectEncoding = Encoding.UTF8;

        //    foreach (var email in mailToEmails)
        //    {
        //        MailAddress emailTo = new MailAddress(email.Key, email.Value);
        //        if (isAllBcc)
        //            message.Bcc.Add(emailTo);
        //        else
        //            message.To.Add(emailTo);
        //    }

        //    message.Bcc.Add(configSection.GetValue<string>("BccEmails"));

        //    message.ReplyToList.Add(new MailAddress(configSection.GetValue<string>("SystemReplyTo"), configSection.GetValue<string>("SystemReplyToName")));

        //    client.Send(message);
        //}

        public bool Send(string subject, string body, string mailToEmail, string mailToName, string attachmentPath = "", bool isAllBcc = false)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var configSection = Configuration.GetSection("MailSettings");
            string BccEmailsStr = configSection.GetValue<string>("BccEmails");
            string SystemReplyTo = configSection.GetValue<string>("SystemReplyTo");
            string SystemReplyToName = configSection.GetValue<string>("SystemReplyToName");

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            //bodyBuilder.TextBody = "Hello World!";
            if (attachmentPath != "")
            {
                var attachmentEntity = bodyBuilder.Attachments.Add(attachmentPath);
                attachmentEntity.ContentDisposition.FileName = System.IO.Path.GetFileName(attachmentPath);
            }

            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(configSection.GetValue<string>("EmailFromName"), configSection.GetValue<string>("EmailFrom"));
            message.From.Add(from);

            MailboxAddress to = new MailboxAddress(mailToName, mailToEmail);
            if (!isAllBcc)
                message.To.Add(to);
            else
                message.Bcc.Add(to);

            var bccEmails = BccEmailsStr.Split(',', options: StringSplitOptions.RemoveEmptyEntries);
            foreach (var email in bccEmails)
            {
                MailboxAddress bcc = new MailboxAddress(email, email);
                message.Bcc.Add(bcc);
            }

            MailboxAddress replyTo = new MailboxAddress(SystemReplyToName, SystemReplyTo);
            message.ReplyTo.Add(replyTo);

            message.Subject = subject;
            message.Body = bodyBuilder.ToMessageBody();

            MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                _logger.LogInformation("SMTP Connect");
                client.Connect(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")), SecureSocketOptions.Auto);
            }
            catch (Exception ex)
            {
                client.Connect(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")), SecureSocketOptions.None);
                _logger.LogError(ex.Message);
            }

            try
            {
                _logger.LogInformation("SMTP Authenticate");
                client.Authenticate(configSection.GetValue<string>("SenderUsername"), configSection.GetValue<string>("SenderPassword"));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            try
            {
                _logger.LogInformation("Sending email");
                client.Send(message);
                client.Disconnect(true);
                //client.Dispose();
                _logger.LogInformation("Email sent");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public bool Send(string subject, string body, Dictionary<string, string> mailToEmails, string attachmentPath = "", bool isAllBcc = false)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var configSection = Configuration.GetSection("MailSettings");
            string BccEmailsStr = configSection.GetValue<string>("BccEmails");
            string SystemReplyTo = configSection.GetValue<string>("SystemReplyTo");
            string SystemReplyToName = configSection.GetValue<string>("SystemReplyToName");

            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            //bodyBuilder.TextBody = "Hello World!";
            if (attachmentPath != "")
            {
                var attachmentEntity = bodyBuilder.Attachments.Add(attachmentPath);
                attachmentEntity.ContentDisposition.FileName = System.IO.Path.GetFileName(attachmentPath);
            }

            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(configSection.GetValue<string>("EmailFromName"), configSection.GetValue<string>("EmailFrom"));
            message.From.Add(from);

            foreach (var email in mailToEmails)
            {
                MailboxAddress emailTo = new MailboxAddress(email.Key, email.Value);
                if (isAllBcc)
                    message.Bcc.Add(emailTo);
                else
                    message.To.Add(emailTo);
            }

            var bccEmails = BccEmailsStr.Split(',', options: StringSplitOptions.RemoveEmptyEntries);
            foreach (var email in bccEmails)
            {
                MailboxAddress bcc = new MailboxAddress(email, email);
                message.Bcc.Add(bcc);
            }

            MailboxAddress replyTo = new MailboxAddress(SystemReplyToName, SystemReplyTo);
            message.ReplyTo.Add(replyTo);

            message.Subject = subject;
            message.Body = bodyBuilder.ToMessageBody();

            MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                _logger.LogInformation("SMTP Connect");
                client.Connect(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")), SecureSocketOptions.Auto);
            }
            catch (Exception ex)
            {
                client.Connect(configSection.GetValue<string>("SmtpMailServer"), int.Parse(configSection.GetValue<string>("SmtpPort")), SecureSocketOptions.None);
                _logger.LogError(ex.Message);
            }

            try
            {
                _logger.LogInformation("SMTP Authenticate");
                client.Authenticate(configSection.GetValue<string>("SenderUsername"), configSection.GetValue<string>("SenderPassword"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            try
            {
                _logger.LogInformation("Sending email");
                client.Send(message);
                client.Disconnect(true);
                //client.Dispose();
                _logger.LogInformation("Email sent");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}

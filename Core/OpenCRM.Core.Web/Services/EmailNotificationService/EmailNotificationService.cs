using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace OpenCRM.Core.Web.Services.EmailNotificationService
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private IConfiguration _configuration;
        public EmailNotificationService(IConfiguration iConfig)
        {
            _configuration = iConfig;
        }
        public bool SendEmail(string email, string subject, string confirmLink)
        {
            try
            {
                if (_configuration.GetValue<bool>("EmailSettings:EmailEnabled"))
                {
                    MailMessage message = new();
                    SmtpClient smtpClient = new();
                    message.From = new MailAddress(_configuration.GetValue<string>("EmailSettings:Email"));
                    message.To.Add(email);
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = confirmLink;

                    smtpClient.Port = 587;
                    smtpClient.Host = "smtp.simply.com";

                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(_configuration.GetValue<string>("EmailSettings:Email"), _configuration.GetValue<string>("EmailSettings:Password"));
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(message);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

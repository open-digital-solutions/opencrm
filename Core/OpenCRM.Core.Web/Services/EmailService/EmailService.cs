using Microsoft.Extensions.Configuration;
using OpenCRM.Core.Web.Models;
using System.Net;
using System.Net.Mail;

namespace OpenCRM.Core.Web.Services.EmailNotificationService
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool SendEmail(string email, string subject, string confirmLink) //TODO: Generalize
        {
            EmailSettings emailSettings = new();
            _configuration.GetRequiredSection("Email").GetRequiredSection("SMTP").Bind(emailSettings);

            try
            {
                if (emailSettings.Enable)
                {
                    MailMessage message = new();
                    SmtpClient smtpClient = new();
                    message.From = new MailAddress(emailSettings.Email);
                    message.To.Add(email);
                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = confirmLink;

                    smtpClient.Port = emailSettings.Port;
                    smtpClient.Host = emailSettings.Server;

                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);
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

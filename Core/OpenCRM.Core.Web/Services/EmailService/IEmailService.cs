namespace OpenCRM.Core.Web.Services.EmailNotificationService
{
    public interface IEmailService
    {
        bool SendEmail(string email, string subject, string confirmLink);
    }
}
namespace OpenCRM.Core.Web.Services.EmailNotificationService
{
    public interface IEmailNotificationService
    {
        bool SendEmail(string email, string subject, string confirmLink);
    }
}
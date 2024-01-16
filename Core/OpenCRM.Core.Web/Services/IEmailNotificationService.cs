namespace OpenCRM.Core.Web.Services
{
    public interface IEmailNotificationService
    {
        bool SendEmail(string email, string subject, string confirmLink);
    }
}
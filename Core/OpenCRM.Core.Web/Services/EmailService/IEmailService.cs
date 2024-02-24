namespace OpenCRM.Core.Web.Services.EmailService
{
    public interface IEmailService
    {
        bool SendEmail(string email, string subject, string confirmLink);
    }
}
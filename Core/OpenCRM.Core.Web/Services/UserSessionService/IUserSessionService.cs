using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.UserSessionService
{
    public interface IUserSessionService
    {
        void GetUserSession(string UserId);
        void SetUserSession(UserSessionModel userSessionModel);
    }
}
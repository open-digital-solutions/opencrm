using OpenCRM.Core.Data;

namespace OpenCRM.Core.Web.Services
{
    public interface IUserSessionService
    {
        void GetUserSession(string UserId);
        Task SetUserSessionAsync(UserEntity user);
    }
}
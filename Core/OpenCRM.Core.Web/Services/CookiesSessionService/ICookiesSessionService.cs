using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services
{
    public interface ICookiesSessionService
    {
        DataSession? GetSession();
        void SetSession(DataSession userSession);
    }
}
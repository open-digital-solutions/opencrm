using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Models;
using System.Text.Json;

namespace OpenCRM.Core.Web.Services
{
    public class CookiesSessionService : ICookiesSessionService
    {
        private readonly string CookieDataSession = "OpenCRM.Session";

        private readonly SignInManager<UserEntity> _signInManager;
        public CookiesSessionService(SignInManager<UserEntity> signInManager)
        {
            _signInManager = signInManager;
        }

        /// <summary>
        /// TODO: Refactor this method. Remove null return
        /// </summary>
        /// <returns></returns>
        public DataSession? GetSession()
        {
            if (_signInManager == null) return null;
            if (_signInManager.Context == null) return null;
            if (_signInManager.Context.Request == null) return null;

            var dataSessionJsonBytesBase64 = _signInManager.Context.Request.Cookies[CookieDataSession];
            if (string.IsNullOrEmpty(dataSessionJsonBytesBase64)) return null;

            var dataSessionJsonBytes = Convert.FromBase64String(dataSessionJsonBytesBase64);
            if (dataSessionJsonBytes == null) return null;

            var dataSessionJson = System.Text.Encoding.ASCII.GetString(dataSessionJsonBytes);
            if (dataSessionJson == null) return null;

            return JsonSerializer.Deserialize<DataSession>(dataSessionJson);
        }

        /// <summary>
        /// TODO: refactor this method
        /// </summary>
        /// <param name="userSession"></param>
        public void SetSession(DataSession userSession)
        {
            CookieOptions options = new()
            {
                Secure = true
            };
            if (_signInManager == null) return;
            if (_signInManager.Context.Response == null) return;

            var dataSessionJson = JsonSerializer.Serialize(userSession);
            var dataSessionJsonBytes = System.Text.Encoding.ASCII.GetBytes(dataSessionJson);
            string dataSessionJsonBytesBase64 = Convert.ToBase64String(dataSessionJsonBytes);

            _signInManager.Context.Response.Cookies.Append(CookieDataSession, dataSessionJsonBytesBase64, options);
        }
    }
}

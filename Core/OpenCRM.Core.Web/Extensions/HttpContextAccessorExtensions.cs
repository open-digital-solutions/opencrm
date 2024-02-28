using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenCRM.Core.Web.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetBaseUrl(this IHttpContextAccessor httpContextAccessor ) {
            if (httpContextAccessor == null) return string.Empty;
            if (httpContextAccessor.HttpContext == null) return string.Empty;
            var scheme = httpContextAccessor.HttpContext.Request.IsHttps == true ? "https://" : "http://";
            return $"{scheme}{httpContextAccessor.HttpContext.Request.Host}";
        }

        public static string GetLanguage(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null) return string.Empty;
            if (httpContextAccessor.HttpContext == null) return string.Empty;

            var userLangs = httpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString();
            var firstLang = userLangs.Split(',').FirstOrDefault();
            return firstLang ?? string.Empty;
        }
    }
}

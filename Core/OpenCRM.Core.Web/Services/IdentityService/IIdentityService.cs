using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Models;

namespace OpenCRM.Core.Web.Services.IdentityService
{
    public interface IIdentityService
    {
        Task<UserModel?> GetLoggedUser();
        Task Logout();
        Task<Tuple<IdentityResult, UserEntity>> RegisterUser(InputRegisterModel Input);
        Task<bool> SendConfirmationEmail(UserEntity user, PageModel page);
        Task<SignInResult> SignInUser(string username, string password, bool rememberMe);
    }
}
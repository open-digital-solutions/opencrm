using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Services.EmailNotificationService;
using System.Text;
using System.Text.Encodings.Web;

namespace OpenCRM.Core.Web.Services.IdentityService
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserStore<UserEntity> _userStore;
        private readonly ILogger<IdentityService> _logger;
        private readonly IEmailService _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(UserManager<UserEntity> userManager,
            IUserStore<UserEntity> userStore,
        SignInManager<UserEntity> signInManager,
        ILogger<IdentityService> logger,
        IEmailService emailSender, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;

        }

        /// <summary>
        /// For general registration User
        /// </summary>
        /// <param name="Input">Input Register Model to use for user registration</param>
        /// <param name="_userManager"></param>
        /// <param name="_userStore"></param>
        /// <param name="_emailStore"></param>
        /// <returns>A Tuple, first parameter contains the result of the user registration and second
        /// parameter contains the user created</returns>
        public async Task<Tuple<IdentityResult, UserEntity>> RegisterUser(InputRegisterModel Input)
        {
            Console.WriteLine(Input.UserExtras);
            var user = CreateUser();
            if (Input.Name != "")
            {
                user.Name = Input.Name;
            }
            if (Input.Lastname != "")
            {
                user.Lastname = Input.Lastname;
            }
            if (Input.Email != "")
            {
                user.Email = Input.Email;
            }

            ////Serialize Later the real Extra Properties!!!
            //var extra = new { Extra1 = "Extra1", Extra2 = "Extra2" };
            //var extraJson = Input.UserExtras == null ? JsonSerializer.Serialize(extra) : Input.UserExtras;
            //user.UserExtras = extraJson;

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, Input.Password);
            return new Tuple<IdentityResult, UserEntity>(result, user);
        }

        public async Task<bool> SendConfirmationEmail(UserEntity user, PageModel page)
        {

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            // var returnUrl = "";

            //var callbackUrl = page.Url.Page(
            //    "Identity/ConfirmEmail",
            //    pageHandler: null,
            //    values: new { area = "", userId, code = encodedCode, returnUrl },
            //    protocol: page.Request.Scheme);

            var callbackUrl = $"{page.Request.Scheme}://{page.Request.Host.Value}/Identity/ConfirmEmail?userId={userId}&code={encodedCode}";

            if (callbackUrl == null || string.IsNullOrEmpty(user.Email))
            {
                return false;
            }

            var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!emailConfirmed)
            {
                await _userManager.ConfirmEmailAsync(user, encodedCode);
            }

            return _emailSender.SendEmail(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        }
        public async Task<SignInResult> SignInUser(string username, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, rememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return result;
            }
            _logger.LogInformation("User logged in.");
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return result;
            }

            var  userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
            _httpContextAccessor?.HttpContext?.SignInAsync(userPrincipal);

            return result;
        }

        private UserEntity CreateUser()
        {
            try
            {
                var instance = Activator.CreateInstance<UserEntity>();
                instance.Data = "{}";
                return instance;
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UserEntity)}'. " +
                    $"Ensure that '{nameof(UserEntity)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}

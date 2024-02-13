using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using OpenCRM.Core.Crypto;
using OpenCRM.Core.Data;
using OpenCRM.Core.Services;
using OpenCRM.Core.Web.Areas.Identity.Models;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.EmailService;
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

            var userCryptoData = RSACryptoService.GetKeyPairs();
            if (userCryptoData != null)
            {
                user.RSAPublic = userCryptoData.GetStringPublicKey();
                user.RSAPrivate = userCryptoData.GetStringPrivateKey();
            }

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
            if (!emailConfirmed) {
                await _userManager.ConfirmEmailAsync(user, encodedCode);
            }

            var emailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!emailConfirmed) {
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

            var userPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
            _httpContextAccessor?.HttpContext?.SignInAsync(userPrincipal);

            return result;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<UserModel?> GetLoggedUser()
        {
            var principal = _httpContextAccessor?.HttpContext?.User;
            if (principal == null) return null;
            var userId = principal.GetNameIdentifierId();
            if (userId == null) return null;
            var userEntity = await _userManager.FindByIdAsync(userId);
            if (userEntity == null) return null;
            return new UserModel
            {
                UserId = userEntity.Id,
                Name = userEntity.Name,
                Lastname = userEntity.Lastname,
                Email = userEntity.Email ?? string.Empty,
                UserName = userEntity.UserName ?? string.Empty
            };

        }

        public async Task<UserEntity?> GetUser(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> ConfirmUserEmail(UserEntity user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task Seed()
        {

            var existedAdminUser = await _userManager.FindByEmailAsync("info@opends.io");
            if (existedAdminUser != null) return;

            var adminUser = new InputRegisterModel
            {
                Email = "info@opends.io",
                Name = "Admin",
                Lastname = "OpenDS",
                Password = "Password.-0",
                ConfirmPassword = "Password.-0"
            };
            var result = await RegisterUser(adminUser);
            if (result.Item1.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(result.Item2);
                var resultEmailCOnfirm = await ConfirmUserEmail(result.Item2, code);
                Console.WriteLine(resultEmailCOnfirm.Succeeded);
            }
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

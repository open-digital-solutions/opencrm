using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OpenCRM.Core.Data;
using OpenCRM.Core.Web.Areas.Identity.Models;
using System.Text.Encodings.Web;
using System.Text;
using System.Text.Json;
using static QRCoder.PayloadGenerator;

namespace OpenCRM.Core.Web.Services.IdentityService
{
    public class IdentityService
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IUserStore<UserEntity> _userStore;
        private readonly ILogger<IdentityService> _logger;
        private readonly IEmailNotificationService _emailSender;

        public IdentityService(UserManager<UserEntity> userManager,
            IUserStore<UserEntity> userStore,
        SignInManager<UserEntity> signInManager,
        ILogger<IdentityService> logger,
        IEmailNotificationService emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;

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

            //Serialize Later the real Extra Properties!!!
            var extra = new { Extra1 = "Extra1", Extra2 = "Extra2" };
            var extraJson = Input.UserExtras == null ? JsonSerializer.Serialize(extra) : Input.UserExtras;
            user.UserExtras = extraJson;

            await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, Input.Password);
            return new Tuple<IdentityResult, UserEntity>(result, user);
        }

        public async Task SendConfirmationEmail(UserEntity user) {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //var callbackUrl = Url.Page(
            //    "/Account/ConfirmEmail",
            //    pageHandler: null,
            //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
            //    protocol: Request.Scheme);

            //_emailSender.SendEmail(Input.Email, "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }

        private UserEntity CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UserEntity>();
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

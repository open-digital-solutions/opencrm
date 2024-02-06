using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OpenCRM.Core.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace OpenCRM.Core.Web.Pages.Identity.RegisterConfirm
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IEmailSender _sender;
        private readonly IConfiguration _configuration;


        public string? Email { get; set; }
        public bool DisplayConfirmAccountLink { get; set; }
        public string? EmailConfirmationUrl { get; set; }

        public IndexModel(UserManager<UserEntity> userManager, IEmailSender sender, IConfiguration configuration)
        {
            _userManager = userManager;
            _sender = sender;
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }
            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = !_configuration.GetValue<bool>("EmailSettings:EmailEnabled");
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page(
                    "/ConfirmEmail/Index",
                    pageHandler: null,
                    values: new { area = "Identity", userId, code, returnUrl },
                    protocol: Request.Scheme);
            }

            return Page();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using OpenCRM.Core.Web.Models;
using OpenCRM.Core.Web.Services.IdentityService;
using System.Text;

namespace OpenCRM.Core.Web.Pages.Identity.ConfirmEmail
{
    public class IndexModel : CorePageModel
    {
        private readonly IIdentityService _identityService;

        public IndexModel(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;


        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _identityService.GetUser(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _identityService.ConfirmUserEmail(user, code);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return Page();
        }
    }
}
